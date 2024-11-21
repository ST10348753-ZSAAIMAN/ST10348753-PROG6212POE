using Microsoft.AspNetCore.Mvc;
using ST10348753_PROG6212POE.Models;
using System.Diagnostics;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace ST10348753_PROG6212POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Simulated in-memory databases for claims and lecturers
        private static List<Claim> claims = new List<Claim>();
        private static List<Lecturer> lecturers = new List<Lecturer>
        {
            new Lecturer { LecturerId = 1, Name = "John Doe", ContactInfo = "john.doe@example.com", HourlyRate = 500 },
            new Lecturer { LecturerId = 2, Name = "Jane Smith", ContactInfo = "jane.smith@example.com", HourlyRate = 450 }
        };

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
        /// Displays the HR Dashboard with approved claims and lecturer data.
        /// </summary>
        [HttpGet]
        public IActionResult HRDashboard()
        {
            ViewBag.ApprovedClaims = claims.Where(c => c.Status == "Approved").ToList();
            ViewBag.Lecturers = lecturers;
            return View();
        }

        /// <summary>
        /// Updates lecturer details based on user input.
        /// </summary>
        [HttpPost]
        public IActionResult UpdateLecturer(int lecturerId, string name, string contactInfo, decimal hourlyRate)
        {
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);
            if (lecturer != null)
            {
                lecturer.Name = name;
                lecturer.ContactInfo = contactInfo;
                lecturer.HourlyRate = hourlyRate;
            }
            return RedirectToAction("HRDashboard");
        }

        /// <summary>
        /// Generates a PDF report of all approved claims.
        /// </summary>
        [HttpPost]
        public IActionResult GenerateReport()
        {
            var approvedClaims = claims.Where(c => c.Status == "Approved").ToList();
            var reportPath = Path.Combine("wwwroot/reports", "ApprovedClaimsReport.pdf");

            Directory.CreateDirectory(Path.GetDirectoryName(reportPath) ?? string.Empty);

            using (var writer = new PdfWriter(reportPath))
            {
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                document.Add(new Paragraph("Approved Claims Report"));
                document.Add(new Paragraph("Generated on: " + DateTime.Now));

                foreach (var claim in approvedClaims)
                {
                    document.Add(new Paragraph($"Claim ID: {claim.ClaimId}, Lecturer: {claim.LecturerName}, Total: {claim.TotalAmount:C}"));
                }
                document.Close();
            }

            return File("/reports/ApprovedClaimsReport.pdf", "application/pdf", "ApprovedClaimsReport.pdf");
        }

        /// <summary>
        /// Displays the status of a specific claim.
        /// </summary>
        public IActionResult ClaimStatus(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);

            if (claim != null)
            {
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
