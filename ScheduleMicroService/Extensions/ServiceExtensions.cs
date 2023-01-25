using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScheduleMicroservice.Application.Consumers;
using ScheduleMicroservice.Application.Service;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Infrastructure;
using ScheduleMicroservice.Infrastructure.Repository;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;

namespace ScheduleMicroService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                    throw new NotImplementedException();
                options.Audience = configuration.GetValue<string>("Routes:Scopes") ??
                                   throw new NotImplementedException();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = configuration.GetValue<string>("Routes:Scopes") ??
                                    throw new NotImplementedException(),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                  throw new NotImplementedException(),
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

    public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ServiceConsumer>();
            x.AddConsumer<ProfileDoctorConsumer>();
            x.AddConsumer<ProfilePatientConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetValue<string>("RabbitMQ:ConnectionStrings") ??
                                 throw new NotImplementedException()));
                cfg.ReceiveEndpoint(configuration.GetValue<string>("RabbitMQ:QueueName:Consumer:Service") ??
                                 throw new NotImplementedException(),
                    e =>
                    {
                        e.ConfigureConsumer<ServiceConsumer>(context);
                    });

                cfg.ReceiveEndpoint(configuration.GetValue<string>("RabbitMQ:QueueName:Consumer:Profile:Doctor") ??
                               throw new NotImplementedException(),
                  e =>
                  {
                      e.ConfigureConsumer<ProfileDoctorConsumer>(context);
                  });

                cfg.ReceiveEndpoint(configuration.GetValue<string>("RabbitMQ:QueueName:Consumer:Profile:Patient") ??
                              throw new NotImplementedException(),
                 e =>
                 {
                     e.ConfigureConsumer<ProfilePatientConsumer>(context);
                 });
            });
        });
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

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }
}