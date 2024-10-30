using FluentValidation;
using Messenger.Database;
using Messenger.Dtos.User;
using Messenger.Mapping;
using Messenger.Models;
using Messenger.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Messenger.Repositories;

public class AuthService
    (
        UserManager<User> _manager,
        SignInManager<User> _signin,
        DBContext _context ,
        [FromKeyedServices("validateLogin")] IValidator<LoginDto> _loginValidator,
        [FromKeyedServices("validateRegistration")] IValidator<RegisterDto> _registerValidator,
        IToken _tokenService,
        ILogger<AuthService> _AuthLogger) : IAuth
{

    private readonly DBContext context = _context;
    private readonly IToken tokenService = _tokenService;
    private readonly UserManager<User> userManager = _manager;
    private readonly SignInManager<User> signinManager = _signin;
    private readonly IValidator<LoginDto> loginValidator = _loginValidator;
    private readonly IValidator<RegisterDto> registerValidator = _registerValidator;
    private readonly ILogger<AuthService> authLogger = _AuthLogger;
    
    
    
    public async Task<User?> Register(RegisterDto dto)
    {
        var result = registerValidator.Validate(dto);
        if(result.IsValid)
        {
            var user = dto.ToDto();
            authLogger.Log(LogLevel.Information , $"user Id is {user.Id}");
            var model = await userManager.CreateAsync(user, dto.password);
            if (model.Succeeded)
            {
                authLogger.Log(LogLevel.Information , "user created!");
            }
            else
            {
                authLogger.Log(LogLevel.Error , "user not created!");
            }

            return user;
        }
        else
        {
            throw new ValidationException(result.Errors);
        }
    }

    public async Task<UserDto?> Login(LoginDto dto)
    {
        var result = loginValidator.Validate(dto);
        if (result.IsValid)
        {
            authLogger.Log(LogLevel.Information,"validation succeeded!");
            var user = await userManager.Users.Where(u => u.Email == dto.email).FirstAsync();
            if (user == null) return null;
            var status = await signinManager.CheckPasswordSignInAsync(user, dto.password, false);
            if (status.Succeeded)
            {
                return new UserDto { Id = user.Id , token = tokenService.CreateToken(user)};
            }
            else
            {
                authLogger.Log(LogLevel.Error,"something went wrong");
                throw new Exception();
            }

        }
        else
        {
            throw new ValidationException(result.Errors);
        }
    }
}