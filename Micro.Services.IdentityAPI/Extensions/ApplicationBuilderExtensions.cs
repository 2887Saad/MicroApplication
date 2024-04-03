using Micro.Services.IdentityAPI.Models;
using Micro.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Micro.Services.IdentityAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection builder)
        {

            var key = Encoding.ASCII.GetBytes(JwtOptions.Secret);
            builder.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer( Options=>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtOptions.Audience,
                };
            });
            return builder;
        }
    }
}
