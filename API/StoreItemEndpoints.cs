using Domain.Interfaces;
using Domain.Models;
using System.Runtime.CompilerServices;

namespace Api;

public static class StoreItemEndpoints
{
    public static void MapStoreItemEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/StoreItems", ListStoreItems);
        app.MapPost("/StoreItems", CreateStoreItem);
        app.MapDelete("/StoreItems/{id}", DeleteStoreItem);
        app.MapGet("/StoreItems/{id}", GetStoreItemById);
        app.MapPut("/StoreItems", UpdateStoreItem);
    }

    public static IResult UpdateStoreItem(IStoreItemRepository repo, StoreItem item)
    {
        try
        {
            if (repo.Update(item))
            {
                return Results.Ok();
            }
            return Results.Problem("Error updating store item.");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static IResult GetStoreItemById(IStoreItemRepository repo, int id)
    {
        try
        {
            StoreItem? item = repo.GetById(id);
            if (item == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static IResult DeleteStoreItem(IStoreItemRepository repo, int id)
    {
        try
        {
            StoreItem? item = repo.GetById(id);
            if (item == null)
            {
                return Results.NotFound();
            }

            repo.Delete(item);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static IResult CreateStoreItem(IStoreItemRepository repo, StoreItem item)
    {
        try
        {
            item.Id = 0;
            repo.Create(item);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static IResult ListStoreItems(IStoreItemRepository repo)
    {
        try
        {
            return Results.Ok(repo.List());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
