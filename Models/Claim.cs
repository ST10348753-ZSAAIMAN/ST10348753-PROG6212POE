namespace ST10348753_PROG6212POE.Models
{
    /// <summary>
    /// Represents a claim submitted by a lecturer for hours worked and payment.
    /// </summary>
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
        /// This property is derived from HoursWorked multiplied by HourlyRate.
        /// </summary>
        public decimal TotalAmount => HoursWorked * HourlyRate;

        /// <summary>
        /// Additional notes or comments on the claim.
        /// This property is optional and can be left null.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Path to the uploaded supporting document for the claim.
        /// This property is optional and can be null if no document is uploaded.
        /// </summary>
        public string? DocumentPath { get; set; }

        /// <summary>
        /// Current status of the claim.
        /// Default status is "Pending", but it can be updated to "Approved" or "Rejected".
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Indicates whether the claim is flagged for review.
        /// A claim is flagged if it violates predefined validation rules.
        /// </summary>
        public bool IsFlagged { get; set; } = false;

        /// <summary>
        /// Reason for flagging the claim.
        /// This property is populated if the claim is flagged.
        /// </summary>
        public string? FlagReason { get; set; }

        /// <summary>
        /// Constructor for creating a Claim object with required and optional details.
        /// </summary>
        /// <param name="claimId">Unique identifier for the claim.</param>
        /// <param name="lecturerName">Name of the lecturer submitting the claim.</param>
        /// <param name="hoursWorked">Total hours worked by the lecturer.</param>
        /// <param name="hourlyRate">Hourly rate for the work done.</param>
        /// <param name="notes">Optional notes or comments for the claim.</param>
        /// <param name="documentPath">Optional path to the uploaded supporting document.</param>
        /// <param name="status">Initial status of the claim. Default is "Pending".</param>
        public Claim(int claimId, string? lecturerName, decimal hoursWorked, decimal hourlyRate, string? notes = null, string? documentPath = null, string status = "Pending")
        {
            // Assign values to properties based on constructor parameters
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
