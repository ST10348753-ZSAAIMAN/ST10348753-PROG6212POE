namespace ST10348753_PROG6212POE.Models
{
    public class Claim
    {
        /// <summary>
        /// Unique identifier for the claim.
        /// </summary>
        public int ClaimId { get; set; }

        /// <summary>
        /// Name of the lecturer submitting the claim.
        /// </summary>
        public string? LecturerName { get; set; }

        /// <summary>
        /// Total hours worked by the lecturer.
        /// </summary>
        public decimal HoursWorked { get; set; }

        /// <summary>
        /// Hourly rate for the lecturer.
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Total calculated amount for the claim.
        /// This property is derived from HoursWorked * HourlyRate.
        /// </summary>
        public decimal TotalAmount => HoursWorked * HourlyRate;

        /// <summary>
        /// Additional notes or comments on the claim.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Path to the uploaded supporting document.
        /// </summary>
        public string? DocumentPath { get; set; }

        /// <summary>
        /// Current status of the claim (e.g., "Pending", "Approved", "Rejected").
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Constructor for creating a Claim object.
        /// </summary>
        /// <param name="claimId">Unique identifier for the claim.</param>
        /// <param name="lecturerName">Name of the lecturer submitting the claim.</param>
        /// <param name="hoursWorked">Total hours worked.</param>
        /// <param name="hourlyRate">Hourly rate for the lecturer.</param>
        /// <param name="notes">Optional notes or comments for the claim.</param>
        /// <param name="documentPath">Optional path to the uploaded document.</param>
        /// <param name="status">Initial status of the claim (default: "Pending").</param>
        public Claim(int claimId, string? lecturerName, decimal hoursWorked, decimal hourlyRate, string? notes = null, string? documentPath = null, string status = "Pending")
        {
            ClaimId = claimId;
            LecturerName = lecturerName;
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
            Notes = notes;
            DocumentPath = documentPath;
            Status = status;
        }
    }
}
