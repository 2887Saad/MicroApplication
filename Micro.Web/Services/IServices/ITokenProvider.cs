namespace Micro.Web.Services.IServices
{
    public interface ITokenProvider
    {
        public void ClearedToken();
        public void SetToken(string token);
        public string GetToken();
    }
}
