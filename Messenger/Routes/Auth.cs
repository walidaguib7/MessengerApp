using FluentValidation;
using Messenger.Dtos.User;
using Messenger.Helpers;
using Messenger.Repositories;
using Messenger.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Messenger.Routes;

public static class Auth
{
    public static void AuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/Auth/register", async (IAuth authService, RegisterDto dto) =>
        {
            try
            {
                var user = await authService.Register(dto);
                if (user == null) return Results.NotFound("user credentials are not invalid!");
                return Results.Created();
            }
            catch (ValidationException e)
            {
                return Results.BadRequest(new ValidationErrorResponse
                    { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithTags("Auth");

        app.MapPost("/api/Auth/login", async (IAuth authService, LoginDto dto) =>
        {
            try
            {
                var user = await authService.Login(dto);
                if (user == null) return Results.NotFound("user not found!");
                return Results.Ok(user);
            }
            catch (ValidationException e)
            {
                return Results.BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage)});
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithTags("Auth");


    }
}