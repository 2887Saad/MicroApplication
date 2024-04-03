using Micro.Services.IdentityAPI.Models;

namespace Micro.Services.IdentityAPI.Service.IService
{
    public interface IJWTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> RoleList);
    }
}
