using Newtonsoft.Json.Linq;

namespace Micro.Services.IdentityAPI.Models
{
    public static class JwtOptions
    {
        public static string Issuer { get; set; } = "The secret is used to verify the token and to create the token";
        public static string Audience { get; set; } = "Micro-client";
        public static string Secret { get; set; } = "Micro-Auth-Api This is the secret of the key and this should be handled secretly";

    }
}
