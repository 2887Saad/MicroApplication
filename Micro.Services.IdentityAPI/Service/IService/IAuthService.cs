using Micro.Services.IdentityAPI.Models.Dto;

namespace Micro.Services.IdentityAPI.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<string> Register(RegisterationRequestDto registerationRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
