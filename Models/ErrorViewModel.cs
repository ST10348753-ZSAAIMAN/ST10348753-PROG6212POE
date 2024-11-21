namespace ST10348753_PROG6212POE.Models
{
    /// <summary>
    /// Represents the model used for handling error information in the application.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request that caused the error.
        /// This property is nullable to account for cases where a request ID is not available.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Determines whether the RequestId should be displayed.
        /// Returns true if RequestId is not null or empty, otherwise false.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Provides a default constructor for the ErrorViewModel.
        /// This allows the creation of an empty instance without initialization.
        /// </summary>
        public ErrorViewModel()
        {
        }

        /// <summary>
        /// Parameterized constructor to initialize the ErrorViewModel with a request ID.
        /// </summary>
        /// <param name="requestId">The unique identifier for the error-causing request.</param>
        public ErrorViewModel(string? requestId)
        {
            RequestId = requestId;
        }
    }
}
