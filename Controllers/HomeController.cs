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
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>The Privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the form for submitting a claim.
        /// </summary>
        /// <returns>The SubmitClaim view.</returns>
        public IActionResult SubmitClaim()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of a claim, including validation and file upload.
        /// </summary>
        /// <param name="hoursWorked">Hours worked by the lecturer.</param>
        /// <param name="hourlyRate">Hourly rate for the work.</param>
        /// <param name="notes">Optional additional notes.</param>
        /// <param name="document">Optional supporting document.</param>
        /// <returns>The SubmitClaim view with success or error message.</returns>
        [HttpPost]
        public IActionResult SubmitClaim(decimal hoursWorked, decimal hourlyRate, string? notes, IFormFile? document)
        {
            string? documentPath = null;

            // Handle file upload if a document is provided
            if (document != null && document.Length > 0)
            {
                // Validate file size
                if (document.Length > 2 * 1024 * 1024) // 2 MB limit
                {
                    ViewBag.Message = "Error: File size exceeds 2MB.";
                    ViewBag.IsError = true;
                    return View();
                }

                // Validate file type
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var fileExtension = Path.GetExtension(document.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ViewBag.Message = "Error: Invalid file type. Allowed types are PDF, DOCX, and XLSX.";
                    ViewBag.IsError = true;
                    return View();
                }

                // Save the document to the server
                documentPath = Path.Combine("wwwroot/uploads", Guid.NewGuid() + fileExtension);
                Directory.CreateDirectory(Path.GetDirectoryName(documentPath) ?? string.Empty);
                using (var stream = new FileStream(documentPath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }
            }

            // Create a new claim and add it to the in-memory database
            var claim = new Claim(
                claimId: claims.Count + 1, // Generate unique claim ID
                lecturerName: "John Doe", // Replace with authenticated user info if available
                hoursWorked: hoursWorked,
                hourlyRate: hourlyRate,
                notes: notes,
                documentPath: documentPath != null ? $"/uploads/{Path.GetFileName(documentPath)}" : null
            );

            claims.Add(claim);

            // Display success message
            ViewBag.Message = "Claim submitted successfully.";
            ViewBag.IsError = false;

            return View();
        }

        /// <summary>
        /// Displays a list of pending claims for approval or rejection.
        /// </summary>
        /// <returns>The ApproveClaim view.</returns>
        public IActionResult ApproveClaim()
        {
            ViewBag.Claims = claims.Where(c => c.Status == "Pending").ToList(); // Filter pending claims
            ViewBag.IsError = false; // Ensure ViewBag.IsError is explicitly set
            return View();
        }

        [HttpPost]
        public IActionResult ApproveClaim(int claimId, string action)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                claim.Status = action == "Approve" ? "Approved" : "Rejected";
                ViewBag.Message = $"Claim {claimId} has been {claim.Status}.";
                ViewBag.IsError = false; // Indicate success
            }
            else
            {
                ViewBag.Message = "Error: Claim not found.";
                ViewBag.IsError = true; // Indicate error
            }

            ViewBag.Claims = claims.Where(c => c.Status == "Pending").ToList(); // Refresh pending claims
            return View();
        }


        /// <summary>
        /// Displays the status of a specific claim.
        /// </summary>
        /// <param name="claimId">ID of the claim to check status.</param>
        /// <returns>The ClaimStatus view.</returns>
        public IActionResult ClaimStatus(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);

            if (claim != null)
            {
                ViewBag.Claim = claim;
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
        /// <returns>The ViewSubmittedClaims view.</returns>
        public IActionResult ViewSubmittedClaims()
        {
            ViewBag.Claims = claims; // Pass all claims
            return View();
        }

        /// <summary>
        /// Handles application errors.
        /// </summary>
        /// <returns>The Error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
