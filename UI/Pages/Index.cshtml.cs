using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorCrudUI.Pages;
public class IndexModel : PageModel
{
    private readonly IStoreItemRepository _repo;

    public List<StoreItem> StoreItems { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Filter { get; set; }

    public IndexModel(IStoreItemRepository repo)
    {
        _repo = repo;
    }

    public void OnGet()
    {
        var items = _repo.List(Filter);
        StoreItems = items.ToList();
    }
}
