using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vini.ModelProject.Api.Configurations
{
    public static class JasonWebTokenConfiguration
    {
        public static void AddJwt(this IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Audience = "http://localhost:4200/";
                    options.ClaimsIssuer = "https://localhost:44367/";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = "https://localhost:44367/",
                        ValidAudience = "http://localhost:4200/",
                        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("SecuritySecretKey"))
                    };
                    options.SaveToken = true;
                });
        }
    }
}
