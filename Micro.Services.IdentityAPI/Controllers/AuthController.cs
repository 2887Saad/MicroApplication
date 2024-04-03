using Micro.Services.IdentityAPI.Models.Dto;
using Micro.Services.IdentityAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Services.IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _IAuthService;
        private readonly ResponseDto _ResponseDto;
        public AuthController(IAuthService IAuthService)
        {
            _IAuthService = IAuthService;
            _ResponseDto = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterationRequestDto registerationRequestDto)
        {
            var ErrorMessages = await _IAuthService.Register(registerationRequestDto);
            if(!string.IsNullOrEmpty(ErrorMessages))
            {
                _ResponseDto.Result = null;
                _ResponseDto.IsSuccess = false;
                _ResponseDto.Message = ErrorMessages;
                return BadRequest(_ResponseDto);
            }
            else
            {
                _ResponseDto.Result = registerationRequestDto;
                _ResponseDto.Message = "";
                return Ok(_ResponseDto);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            LoginResponseDto loginresponseDto =  await _IAuthService.Login(loginRequestDto);
            if(loginresponseDto.User == null)
            {
                _ResponseDto.IsSuccess = false;
                _ResponseDto.Result = null;
                _ResponseDto.Message = "Username or password is Incorrect";
                return BadRequest(_ResponseDto);
            }
            _ResponseDto.Result = loginresponseDto;
            _ResponseDto.Message = "";
            return Ok(_ResponseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody]RegisterationRequestDto registerationRequestDto)
        {
            bool IsAssigned= await _IAuthService.AssignRole(registerationRequestDto.Email, registerationRequestDto.Role.ToUpper());
            if (!IsAssigned)
            {
                _ResponseDto.IsSuccess = false;
                _ResponseDto.Result = null;
                _ResponseDto.Message = "Username or password is Incorrect";
                return BadRequest(_ResponseDto);
            }
            _ResponseDto.Result = null;
            _ResponseDto.Message = "";
            return Ok(_ResponseDto);
        }
    }
}
