using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using Micro.Web.Utility;

namespace Micro.Web.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto() 
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = registerationRequestDto,
                Url = SD.AuthApiBase + "api/Auth/Register"
            },withBearer:false);
        }

        public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthApiBase + "api/Auth/Login"
            },withBearer:false);
        }

        public async Task<ResponseDto> AssignRole(RegisterationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = registerationRequestDto,
                Url = SD.AuthApiBase + "api/Auth/AssignRole"
            });
        }
    }
}
