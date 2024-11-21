// Zinedean Saaiman
// ST10348753
// Group: 2

// References: 
// https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0
// https://getbootstrap.com/docs/5.1/getting-started/introduction/
// https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/dotnet/standard/class-libraries
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/

using Microsoft.AspNetCore.Mvc;
using ST10348753_PROG6212POE.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ST10348753_PROG6212POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Add this static list for simulating a database
        private static List<Claim> claims = new List<Claim>(); // In-memory list of claims

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

        // Display the Submit Claim form
        public IActionResult SubmitClaim()
        {
            // This displays the form where lecturers submit their claims
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(decimal hoursWorked, decimal hourlyRate, string notes, IFormFile document)
        {
            // Validate and save the uploaded document
            string documentPath = null; // Initialize document path
            if (document != null && document.Length > 0)
            {
                // Validate file size (2MB limit)
                if (document.Length > 2 * 1024 * 1024)
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

                // Save the file to a unique path
                documentPath = Path.Combine("wwwroot/uploads", Guid.NewGuid() + fileExtension);
                Directory.CreateDirectory(Path.GetDirectoryName(documentPath));
                using (var stream = new FileStream(documentPath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }
            }

            // Create a new claim and add it to the list
            var claim = new Claim
            {
                ClaimId = claims.Count + 1, // Auto-generate ClaimId
                LecturerName = "John Doe", // Replace with actual lecturer name when authentication is added
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                TotalAmount = hoursWorked * hourlyRate, // Calculate total amount
                Notes = notes,
                DocumentPath = documentPath != null ? $"/uploads/{Path.GetFileName(documentPath)}" : null,
                Status = "Pending" // Default status
            };
            claims.Add(claim); // Add the new claim to the list

            ViewBag.Message = "Claim submitted successfully.";
            ViewBag.IsError = false;

            return View();
        }




        // This is the HomePage action, which will render the custom home page
        public IActionResult HomePage()
        {
            return View();
        }

        // Display the Approve Claim form
        public IActionResult ApproveClaim()
        {
            ViewBag.Claims = claims;
            return View("SubmittedClaims");
        }


        // Handle the approval or rejection of a claim
        [HttpPost]
        public IActionResult ApproveClaim(int claimId, string action)
        {
            // Find the claim by ClaimId
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                // Update the status based on the action
                claim.Status = action == "Approve" ? "Approved" : "Rejected";
                ViewBag.Message = $"Claim {claimId} has been {claim.Status}.";
            }
            else
            {
                ViewBag.Message = "Error: Claim not found.";
            }

            ViewBag.Claims = claims; // Pass the updated claims list to the view
            return View("SubmittedClaims");
        }




        // Display the Document Upload form
        public IActionResult UploadDocument()
        {
            // This displays the document upload form
            return View();
        }

        [HttpPost]
        public IActionResult UploadDocumentPost(IFormFile document)
        {
            if (document == null || document.Length == 0)
            {
                ViewBag.Message = "Please select a valid file.";
                return View("UploadDocument");
            }

            if (document.Length > 2 * 1024 * 1024)
            {
                ViewBag.Message = "Error: File size exceeds 2MB.";
                return View("UploadDocument");
            }

            var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
            var fileExtension = Path.GetExtension(document.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ViewBag.Message = "Error: Invalid file type. Only PDF, DOCX, and XLSX are allowed.";
                return View("UploadDocument");
            }

            var filePath = Path.Combine("wwwroot/uploads", document.FileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                document.CopyTo(stream);
            }

            ViewBag.Message = "File uploaded successfully.";
            return View("UploadDocument");
        }




        // Display the status of a specific claim
        public IActionResult ClaimStatus(int claimId)
        {
            // Retrieve the claim details based on the provided Claim ID
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);

            // Check if the claim exists
            if (claim != null)
            {
                // Pass the claim details to the view using ViewBag
                ViewBag.ClaimId = claim.ClaimId;
                ViewBag.Lecturer = claim.LecturerName;
                ViewBag.Status = claim.Status;
            }
            else
            {
                // Display an error message if the claim is not found
                ViewBag.Message = "Claim not found.";
            }

            return View(); // Render the Claim Status view
        }


        // Display a list of all submitted claims
        public IActionResult ViewSubmittedClaims()
        {
            // Pass the list of claims to the view
            ViewBag.Claims = claims;
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
