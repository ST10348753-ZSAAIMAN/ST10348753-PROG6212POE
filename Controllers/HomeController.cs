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
        private static List<dynamic> claims = new List<dynamic>(); // Simulating a database

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

        // Handle claim submission along with a file upload
        [HttpPost]
        public IActionResult SubmitClaim(decimal hoursWorked, decimal hourlyRate, string notes, IFormFile document)
        {
            if (document != null && document.Length > 0)
            {
                // Validate file size and type
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var fileExtension = Path.GetExtension(document.FileName).ToLower();

                if (document.Length > 2 * 1024 * 1024)
                {
                    ViewBag.Message = "Error: File size exceeds 2MB.";
                    ViewBag.IsError = true;
                    return View();
                }

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ViewBag.Message = "Error: Invalid file type. Allowed types are PDF, DOCX, and XLSX.";
                    ViewBag.IsError = true;
                    return View();
                }

                // Save file
                var filePath = Path.Combine("wwwroot/uploads", document.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }

                ViewBag.FileMessage = $"Document {document.FileName} uploaded successfully.";
            }

            // Save claim details in the in-memory list
            var totalAmount = hoursWorked * hourlyRate;
            claims.Add(new
            {
                ClaimId = claims.Count + 1,
                LecturerName = "John Doe", // Example name; integrate authentication later
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                TotalAmount = totalAmount,
                Notes = notes,
                DocumentPath = document != null ? $"/uploads/{document.FileName}" : null,
                Status = "Pending"
            });

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
            // Find the claim in the in-memory storage using Claim ID
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);

            // Check if the claim exists
            if (claim != null)
            {
                // Update the claim status based on the action (Approve or Reject)
                claim.Status = action == "Approve" ? "Approved" : "Rejected";

                // Display a success message indicating the updated status
                ViewBag.Message = $"Claim {claimId} has been {claim.Status}.";
            }
            else
            {
                // Display an error message if the claim is not found
                ViewBag.Message = "Claim not found.";
            }

            // Pass the updated claims list to the view
            ViewBag.Claims = claims;
            return View("ViewSubmittedClaims"); // Redirect to the claims list view
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
            if (document != null && document.Length > 0)
            {
                try
                {
                    // Restrict file size to 2MB
                    if (document.Length > 2 * 1024 * 1024)
                    {
                        ViewBag.Message = "File size exceeds 2 MB.";
                        return View("UploadDocument");
                    }

                    // Restrict file types to PDF, DOCX, and XLSX
                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                    var fileExtension = Path.GetExtension(document.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ViewBag.Message = "Invalid file type. Only PDF, DOCX, and XLSX are allowed.";
                        return View("UploadDocument");
                    }

                    // Save the file
                    var filePath = Path.Combine("wwwroot/uploads", document.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        document.CopyTo(stream);
                    }

                    ViewBag.Message = "File uploaded successfully.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error uploading document: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "Please select a valid file.";
            }

            // Return the UploadDocument view to display the message
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
            // Pass the in-memory claims list to the view using ViewBag
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
