using Microsoft.AspNetCore.Mvc;
using ST10348753_PROG6212POE.Models;
using System.Diagnostics;

namespace ST10348753_PROG6212POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Simulated in-memory database for claims
        private static List<Claim> claims = new List<Claim>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the form to submit a claim.
        /// </summary>
        public IActionResult SubmitClaim()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of a claim with validations and file upload.
        /// </summary>
        [HttpPost]
        public IActionResult SubmitClaim(decimal hoursWorked, decimal hourlyRate, string? notes, IFormFile? document)
        {
            string? documentPath = null;

            // Validate and handle file upload
            if (document != null && document.Length > 0)
            {
                // File size validation
                if (document.Length > 2 * 1024 * 1024)
                {
                    ViewBag.Message = "Error: File size exceeds 2MB.";
                    ViewBag.IsError = true;
                    return View();
                }

                // File type validation
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var fileExtension = Path.GetExtension(document.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ViewBag.Message = "Error: Invalid file type. Allowed types are PDF, DOCX, and XLSX.";
                    ViewBag.IsError = true;
                    return View();
                }

                // Save the file to the server
                documentPath = Path.Combine("wwwroot/uploads", Guid.NewGuid() + fileExtension);
                Directory.CreateDirectory(Path.GetDirectoryName(documentPath) ?? string.Empty);
                using (var stream = new FileStream(documentPath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }
            }

            // Create a new claim and add it to the in-memory database
            var claim = new Claim(
                claimId: claims.Count + 1,
                lecturerName: "John Doe", // Placeholder for lecturer name
                hoursWorked: hoursWorked,
                hourlyRate: hourlyRate,
                notes: notes,
                documentPath: documentPath != null ? $"/uploads/{Path.GetFileName(documentPath)}" : null
            );

            // Apply flagging logic
            if (hoursWorked > 100) // Example threshold
            {
                claim.IsFlagged = true;
                claim.FlagReason = "Hours worked exceed the maximum allowable threshold.";
            }
            else if (hourlyRate > 1000) // Example threshold
            {
                claim.IsFlagged = true;
                claim.FlagReason = "Hourly rate exceeds the reasonable threshold.";
            }

            claims.Add(claim);

            ViewBag.Message = "Claim submitted successfully.";
            ViewBag.IsError = false;

            return View();
        }

        /// <summary>
        /// Displays claims pending approval or flagged for review.
        /// </summary>
        public IActionResult ApproveClaim()
        {
            ViewBag.Claims = claims.Where(c => c.Status == "Pending").ToList();
            ViewBag.IsError = false;
            return View();
        }

        /// <summary>
        /// Handles claim approval or rejection.
        /// </summary>
        [HttpPost]
        public IActionResult ApproveClaim(int claimId, string action)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                if (claim.IsFlagged && action == "Approve")
                {
                    ViewBag.Message = $"Claim {claimId} is flagged: {claim.FlagReason}. Please resolve before approving.";
                    ViewBag.IsError = true;
                }
                else
                {
                    claim.Status = action == "Approve" ? "Approved" : "Rejected";
                    ViewBag.Message = $"Claim {claimId} has been {claim.Status}.";
                    ViewBag.IsError = false;
                }
            }
            else
            {
                ViewBag.Message = "Error: Claim not found.";
                ViewBag.IsError = true;
            }

            ViewBag.Claims = claims.Where(c => c.Status == "Pending").ToList();
            return View();
        }

        /// <summary>
        /// Displays the status of a specific claim.
        /// </summary>
        /// <param name="claimId">ID of the claim to check the status.</param>
        /// <returns>The ClaimStatus view.</returns>
        public IActionResult ClaimStatus(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);

            if (claim != null)
            {
                // Pass claim details to the view via ViewBag
                ViewBag.ClaimId = claim.ClaimId;
                ViewBag.Lecturer = claim.LecturerName;
                ViewBag.Status = claim.Status;
            }
            else
            {
                ViewBag.Message = "Claim not found.";
            }

            return View();
        }

        /// <summary>
        /// Displays all submitted claims.
        /// </summary>
        public IActionResult ViewSubmittedClaims()
        {
            ViewBag.Claims = claims;
            return View();
        }

        /// <summary>
        /// Handles errors in the application.
        /// </summary>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
