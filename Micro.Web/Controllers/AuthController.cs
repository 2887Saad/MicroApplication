using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using Micro.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Micro.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _ITokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _ITokenProvider = tokenProvider;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            var Rolelist = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleCustomer, Value = SD.RoleCustomer},
                new SelectListItem { Text=SD.RoleAdmin, Value = SD.RoleAdmin}
            };
            ViewBag.RoleList = Rolelist;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestDto registerationRequestDto)
        {
            ResponseDto? result1 = await _authService.Register(registerationRequestDto);
            ResponseDto? result2 = await _authService.AssignRole(registerationRequestDto);
            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
             ResponseDto responseDto = await _authService.Login(loginRequestDto);
             
            if(responseDto.IsSuccess && responseDto.Result != null)
            {
                LoginResponseDto? loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                if (loginResponseDto != null)
                {
                    await SignInUser(loginResponseDto);
                    string token = loginResponseDto.Token;
                    _ITokenProvider.SetToken(token);
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            _ITokenProvider.ClearedToken();
            return RedirectToAction("Index", "Home");
        }
        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(loginResponseDto.Token);
            var claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u=>u.Type == JwtRegisteredClaimNames.Email).Value));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u=>u.Type == JwtRegisteredClaimNames.Name).Value
                ));
            claims.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value
                ));
            claims.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value
                ));
            var principal = new ClaimsPrincipal(claims);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme , principal);   
        }
    }
}
