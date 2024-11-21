namespace ST10348753_PROG6212POE.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; } // Unique identifier
        public string Name { get; set; } = string.Empty; // Default value
        public string ContactInfo { get; set; } = string.Empty; // Default value
        public decimal HourlyRate { get; set; } // Current hourly rate
    }
}
