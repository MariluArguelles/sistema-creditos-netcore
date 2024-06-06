using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS API",
                Version = "v1",
                Description = "Punto de venta API 2022",
                TermsOfService = new Uri("http://opensource.org/licenses/NIT"),
                Contact = new OpenApiContact
                {
                    Name = "Sir Tech S.A.C.",
                    Email = "sirtech@gmail.com",
                    Url = new Uri("http://opensource.org/licenses/NIT")
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("http://opensource.org/licenses/NIT")
                }
            };

            services.AddSwaggerGen(x => {
                openApi.Version = "v1";
                x.SwaggerDoc("v1",openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                x.AddSecurityDefinition(securityScheme.Reference.Id,securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme,new string[]{ } }
                });
            });
            return services;
        }
    }
}
