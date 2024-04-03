namespace Micro.Services.CompanyAPI.Utility
{
    public static class ErrorMessages
    {
        public const string RequiredField  = "This field is required. Please provide a value";
        public const string InvalidEmailFormat  = "Please enter a valid email address";
        public const string FormSubmission = "Sorry, we couldn't process your request at the moment. Please try again Later.";
        public const string FileType = "Unsupported file type. Please upload a valid file jpg,jpeg,png";
        public const string FileSize = "File size exceeds the allowed limit. Please upload a smaller file";
        public const string StringLength = "The length should be between 3 to 100 characters in length";
        public const string NotFound = "Record Not Found";
        public const string DataNotAvailable = "The request does not contain data";
    }
}
