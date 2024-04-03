using Micro.Web.Services.IServices;

namespace Micro.Web.Services.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public TokenProvider(IHttpContextAccessor iHttpContextAccessor)
        {
            _IHttpContextAccessor = iHttpContextAccessor;
        }
        public void ClearedToken()
        {
            _IHttpContextAccessor.HttpContext?.Response.Cookies.Delete("JWTToken");
        }

        public string GetToken()
        {
            string? token = null;
            bool? hasToken = _IHttpContextAccessor.HttpContext?.Request.Cookies.TryGetValue("JWTToken",out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _IHttpContextAccessor.HttpContext?.Response.Cookies.Append("JWTToken", token);
        }
    }
}
