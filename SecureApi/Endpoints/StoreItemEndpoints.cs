using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Runtime.CompilerServices;

namespace SecureApi.Endpoints;

public static class StoreItemEndpoints
{
    public static void MapStoreItemEndpoints(this IEndpointRouteBuilder app)
    {
        // unsecured endpoints
        app.MapGet("/SecureStoreItems", ListStoreItems);

        // make a repository for secured api
        // i think i can use code to generate token instead of endpoint
        // how do i know when i have to get a token or a new token?

        // i feel like i should be able to move the auth service
        // from data to this project
        // maybe dont make a service and just put code in a method
        // in the static endpoint class
        // i know that not good practice but ive been avoiding doing services
        // this far 

        // shared admin constant?

        // temp secured for easy testing
        app.MapGet("/SecureStoreItems/{id}", GetStoreItemById)
            .RequireAuthorization(config => config.RequireRole("Admin"));

        // secured endpoints
        app.MapPost("/SecureStoreItems", CreateStoreItem)
            .RequireAuthorization(config => config.RequireRole("Admin"));
        app.MapDelete("/SecureStoreItems/{id}", DeleteStoreItem)
            .RequireAuthorization(config => config.RequireRole("Admin"));
        app.MapPut("/SecureStoreItems", UpdateStoreItem)
            .RequireAuthorization(config => config.RequireRole("Admin"));
    }

    public static IResult UpdateStoreItem(IStoreItemRepository repo, StoreItem item)
    {
        if (repo.Update(item))
        {
            return Results.Ok();
        }
        return Results.Problem("Error updating store item.");
    }

    public static IResult GetStoreItemById(IStoreItemRepository repo, int id)
    {
        StoreItem? item = repo.GetById(id);
        if (item == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(item);
    }

    public static IResult DeleteStoreItem(IStoreItemRepository repo, int id)
    {
        StoreItem? item = repo.GetById(id);
        if (item == null)
        {
            return Results.NotFound();
        }

        repo.Delete(item);
        return Results.Ok();
    }

    public static IResult CreateStoreItem(IStoreItemRepository repo, StoreItem item)
    {
        item.Id = 0;
        repo.Create(item);
        return Results.Ok();
    }

    public static IResult ListStoreItems(IStoreItemRepository repo)
    {
        return Results.Ok(repo.List());
    }
}
