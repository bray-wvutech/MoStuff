using Domain.Interfaces;
using Domain.Models;
using System.Net.Http.Json;

namespace Data.Repositories.FromApi;

// usually this kind of thing would be presented as a service
// where you had multiple layers of abstraction
// (ie services called by the UI, which call repositories)
// but I am presenting this just as a repository to keeps things
// a little simpler with only one layer of abstraction
// and just concentrate on that concept
public class StoreItemRepositoryFromApi : IStoreItemRepository
{
    private readonly HttpClient _httpClient;

    public StoreItemRepositoryFromApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void Create(StoreItem item)
    {
        try
        {
            _httpClient.PostAsJsonAsync("StoreItems", item);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete(StoreItem item)
    {
        try
        {
            _httpClient.DeleteAsync(_httpClient.BaseAddress + 
                $"StoreItems/{item.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public StoreItem? GetById(int id)
    {
        try
        {
            var item = _httpClient.GetFromJsonAsync<StoreItem>(
                _httpClient.BaseAddress + $"StoreItems/{id}");
            return item.Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public IEnumerable<StoreItem> List(string? filter = "")
    {
        try
        {
            var items = _httpClient.GetFromJsonAsync<IEnumerable<StoreItem>>("StoreItems");
            if ((items == null) || (items.Result == null))
            {
                return Enumerable.Empty<StoreItem>();
            }
            if (string.IsNullOrEmpty(filter))
            {
                return items.Result.ToList();
            }
            return items.Result.Where(i => i.Name.ToLower().Contains(
                filter.ToLower())).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Enumerable.Empty<StoreItem>();
        }
    }

    public bool Update(StoreItem item)
    {
        try
        {
            _httpClient.PutAsJsonAsync(
                _httpClient.BaseAddress + "StoreItems", item);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}
