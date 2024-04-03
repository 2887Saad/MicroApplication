using Micro.Services.IdentityAPI.Data;
using Micro.Services.IdentityAPI.Models;
using Micro.Services.IdentityAPI.Models.Dto;
using Micro.Services.IdentityAPI.Service.IService;
using Micro.Web.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Micro.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly DBContext _DBContext;
        private readonly IJWTokenGenerator _JWTokenGenerator;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DBContext dBContext, IJWTokenGenerator jWTokenGenerator)
        {
            _UserManager = userManager;
            _RoleManager = roleManager;
            _DBContext = dBContext;
            _JWTokenGenerator = jWTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            if(String.IsNullOrEmpty(roleName) ) roleName = SD.RoleCustomer;
            var user = _DBContext.ApplicationUsers.FirstOrDefault(u => u.Email == email.ToLower());
            if (user != null)
            {
                if(!await _RoleManager.RoleExistsAsync(roleName))
                {
                    await _RoleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _UserManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _DBContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == loginRequestDto.Email.ToLower());

            bool isValid = await _UserManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }
            var roles = await _UserManager.GetRolesAsync(user);
            var token = _JWTokenGenerator.GenerateToken(user,roles);
            //if user was found , Generate JWT Token

            UserDto userDTO = new()
            {
                Email = user.Email,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;

        }
     
        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            try
            {
                ApplicationUser applicationUser = new()
                {
                    UserName = registerationRequestDto.Email,
                    Email = registerationRequestDto.Email,
                    PhoneNumber = registerationRequestDto.PhoneNumber,
                    NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                    FirstName = registerationRequestDto.FirstName,
                    LastName = registerationRequestDto.LastName,
                };
                var result = await _UserManager.CreateAsync(applicationUser, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var UserToReturn = await _DBContext.ApplicationUsers.Where(x => x.Email == registerationRequestDto.Email).FirstAsync();
                    UserDto user = new()
                    {
                        LastName = UserToReturn.LastName,
                        Email = UserToReturn.Email,
                        PhoneNumber = UserToReturn.PhoneNumber,
                        Id = UserToReturn.Id
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
