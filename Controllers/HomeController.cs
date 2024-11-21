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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

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

            claims.Add(claim);

            ViewBag.Message = "Claim submitted successfully.";
            ViewBag.IsError = false;

            return View();
        }

        public IActionResult ApproveClaim()
        {
            ViewBag.Claims = claims.Where(c => c.Status == "Pending").ToList();
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
            }
            else
            {
                ViewBag.Message = "Error: Claim not found.";
            }

            ViewBag.Claims = claims.Where(c => c.Status == "Pending").ToList();
            return View();
        }

        public IActionResult ViewSubmittedClaims()
        {
            ViewBag.Claims = claims;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
