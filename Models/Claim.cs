namespace ST10348753_PROG6212POE.Models
{
    public class Claim
    {
        public int ClaimId { get; set; } // Unique identifier for the claim
        public string LecturerName { get; set; } // Name of the lecturer submitting the claim
        public decimal HoursWorked { get; set; } // Total hours worked by the lecturer
        public decimal HourlyRate { get; set; } // Hourly rate for the lecturer
        public decimal TotalAmount { get; set; } // Total calculated amount (HoursWorked * HourlyRate)
        public string Notes { get; set; } // Additional notes or comments on the claim
        public string DocumentPath { get; set; } // Path to the uploaded supporting document
        public string Status { get; set; } // Status of the claim (e.g., "Pending", "Approved", "Rejected")
    }
}
