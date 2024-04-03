using Micro.Services.IdentityAPI.Models;
using Micro.Services.IdentityAPI.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Micro.Services.IdentityAPI.Service
{
    public class JWTokenGenerator : IJWTokenGenerator
    {
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> RoleList)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtOptions.Secret);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name,$"{applicationUser.FirstName}+{applicationUser.LastName}"),
            };
            claims.AddRange(RoleList.Select(role => new Claim(ClaimTypes.Role,role)));
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = JwtOptions.Audience,
                Issuer = JwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
