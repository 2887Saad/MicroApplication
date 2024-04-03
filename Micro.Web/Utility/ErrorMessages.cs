namespace Micro.Web.Utility
{
    public class ErrorMessages
    {
        public const string NotFound = "Not Found";
        public const string AccessDenied = "Access Denied";
        public const string Unauthorized = "Unauthorized";
        public const string InternalServerError = "Internal Server Error";

        //Company
        public const string RequiredField = "This field is required. Please provide a value";
        public const string InvalidEmailFormat = "Please enter a valid email address";
        public const string FormSubmission = "Sorry, we couldn't process your request at the moment. Please try again Later.";
        public const string FileType = "Unsupported file type. Please upload a valid file jpg,jpeg,png";
        public const string FileSize = "File size exceeds the allowed limit. Please upload a smaller file";
        public const string StringLength = "The length should be between 3 to 100 characters in length";
    }
}
