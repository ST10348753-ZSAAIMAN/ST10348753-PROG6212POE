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

namespace ST10348753_PROG6212POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        // Handle the form submission for Submit Claim
        [HttpPost]
        public IActionResult SubmitClaim(decimal hoursWorked, decimal hourlyRate, string notes)
        {
            // Calculate the total amount based on hours worked and hourly rate
            decimal totalAmount = hoursWorked * hourlyRate;

            // Simulate saving the claim details to a database (for demonstration purposes)
            // In the future, you would connect this to a database for actual storage

            // Set a success message to be displayed to the user
            ViewBag.Message = $"Claim submitted for {hoursWorked} hours at {hourlyRate:C} per hour. " +
                              $"Total amount: {totalAmount:C}. Additional notes: {notes}";

            // Return the same view to display the success message
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
            // This displays the form where Programme Coordinators approve claims
            return View();
        }

        // Handle the approval or rejection of claims
        [HttpPost]
        public IActionResult ApproveClaim(string action)
        {
            // Simulate an approval/rejection process
            ViewBag.Message = action == "Approve" ? "Claim approved." : "Claim rejected.";
            return View();
        }

        // Display the Document Upload form
        public IActionResult UploadDocument()
        {
            // This displays the document upload form
            return View();
        }

        // Handle the file upload process
        [HttpPost]
        public IActionResult UploadDocumentPost()
        {
            // Simulate the upload process
            ViewBag.Message = "Document uploaded successfully.";
            return View();
        }

        // Display the Claim Status
        public IActionResult ClaimStatus()
        {
            // This displays the current status of the claim
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
