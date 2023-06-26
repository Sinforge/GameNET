using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Auth;
using System.Reflection;
using System.Text;

namespace Shared.Extentions
{
    public static class CommonServiceCollectionExtentions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    var jwtConfig = configuration.GetSection("Audience");
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig["Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig["Iss"],
                        ValidateAudience = true,
                        ValidAudience = jwtConfig["Aud"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,

                    };
                });
            services.AddAuthorization();
            services.Configure<Audience>(configuration.GetSection("Audience"));

        }
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            var openApi = configuration.GetRequiredSection("OpenApi");
            services.AddSwaggerGen(options =>
            {
                var document = openApi.GetRequiredSection("Document");
                var version  = document.GetValue<string>("Version") ?? "v1";
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = document.GetValue<string>("Title"),
                    Version = version,
                    Description = document.GetValue<string>("Description")

                });
                
            });
        }

    }
}
