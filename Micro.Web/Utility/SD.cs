using static System.Net.WebRequestMethods;

namespace Micro.Web.Utility
{
    public class SD
    {
        public const string AuthApiBase = "https://localhost:7145/";
        public const string ApiBase = "https://localhost:7240/";

        public enum ApiType
        {
            POST,
            GET,
            PUT,
            DELETE
        }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

    }
}
