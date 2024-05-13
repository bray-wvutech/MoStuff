using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace SecureApi.Endpoints;

public static class AuthEndpoint
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/Auth", Authenticate);
    }   

    private static IResult Authenticate(IAuthTokenService authTokenService, 
        string username, string password)
    {
        var token = authTokenService.GetTokenAsync(username, password);
        return Results.Ok(token.Result);
    }
}
