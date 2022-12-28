using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScheduleMicroservice.Application.Repository;
using ScheduleMicroservice.Application.Repository.Abstractions;
using ScheduleMicroservice.Application.Service;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Infrastructure;
using ScheduleMicroService.Api.Extensions;

namespace ServiceMicroService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                   {
                       options.Authority = configuration.GetValue<string>("Routes:AuthorityRoute") ?? throw new NotImplementedException();
                       options.Audience = configuration.GetValue<string>("Routes:Scopes") ?? throw new NotImplementedException();
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateAudience = true,
                           ValidAudience = configuration.GetValue<string>("Routes:Scopes") ?? throw new NotImplementedException(),
                           ValidateIssuer = true,
                           ValidIssuer = configuration.GetValue<string>("Routes:AuthorityRoute") ?? throw new NotImplementedException(),
                           ValidateLifetime = true
                       };
                   });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSingleton<DapperContext>();
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<IAppointmentsService, AppointmentsService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        { Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });

            });
        }

    }
}
