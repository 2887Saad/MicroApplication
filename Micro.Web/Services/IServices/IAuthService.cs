using Micro.Web.Models.Dto;

namespace Micro.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegisterationRequestDto registerationRequestDto);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRole(RegisterationRequestDto registerationRequestDto);


    }
}
