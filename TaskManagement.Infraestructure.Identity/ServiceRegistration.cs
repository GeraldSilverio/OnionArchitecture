using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TaskManagement.Core.Application.Interfaces.Services;
using TaskManagement.Core.Application.Wrappers;
using TaskManagement.Core.Domain.Settings;
using TaskManagement.Infraestructure.Identity.Context;
using TaskManagement.Infraestructure.Identity.Interfaces;
using TaskManagement.Infraestructure.Identity.Services;

namespace TaskManagement.Infraestructure.Identity;

public static class ServiceRegistration
{
    public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ContextConfiguration(services, configuration);

        #region Identity

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Login";
            options.AccessDeniedPath = "/Login/AccessDenied";
        });

        services.AddAuthentication();
        services.AddAuthorization();

        #endregion

        #region JWToken

        services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidAudience = configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
            };

            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    return c.Response.WriteAsync(c.Exception.ToString());
                },
                OnChallenge = c =>
                {
                    c.HandleResponse();
                    c.Response.StatusCode = 401;
                    c.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorize"));
                    return c.Response.WriteAsync(result);
                },
                OnForbidden = c =>
                {
                    c.Response.StatusCode = 403;
                    c.Response.ContentType = "application/json";
                    var result =
                        JsonConvert.SerializeObject(
                            new Response<string>("You are not Authorize to access this resource"));
                    return c.Response.WriteAsync(result);
                },
            };
        });

        #endregion

        ServiceConfiguration(services);
    }

    #region "Private Methods"

    private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration)
    {
        #region IdentityContext

        services.AddDbContext<IdentityContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
        });

        #endregion
    }

    private static void ServiceConfiguration(IServiceCollection services)
    {
        #region Services

        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IJwtServices, JwtServices>();

        #endregion
    }

    #endregion
}