using System.Collections.Immutable;
using FluentValidation;
using Messenger.Database;
using Messenger.Dtos.User;
using Messenger.Models;
using Messenger.Repositories;
using Messenger.Services;
using Messenger.Validations.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Messenger.Config;

public static class ServicesContainer
{
    public static void AddServices(this IServiceCollection services)
    {
        services.Addvalidation();
        services.AddSingleton<IToken,TokenService>();
        services.AddScoped<IAuth, AuthService>();
    }

    public static void Addvalidation(this IServiceCollection services)
    {
        services.AddKeyedScoped<IValidator<LoginDto> , LoginValidation>("validateLogin");
        services.AddKeyedScoped<IValidator<RegisterDto> , RegisterValidation>("validateRegistration");
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;

    
            }).AddEntityFrameworkStores<DBContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<DBContext>(options =>
            options.UseSqlite("DataSource=app.db")
        );
    }

    public static void AddAuthentication(this IServiceCollection services , WebApplicationBuilder builder)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                    options.DefaultForbidScheme =
                        options.DefaultScheme =
                            options.DefaultSignInScheme =
                                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {

                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"]))


            };
        });
    }
    
    public static void AddRedis(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<IConnectionMultiplexer>
            (
                x => ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("RedisConnectionString"))
            )
            ;
    }
    
    public static void AddMailing(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var smtpSettings = builder.Configuration.GetSection("Smtp");
        services.AddSingleton(smtpSettings);
    }
}