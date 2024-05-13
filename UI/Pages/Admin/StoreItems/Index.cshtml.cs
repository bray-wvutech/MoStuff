using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Interfaces;
using Domain.Models;

namespace RazorCrudUI.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IStoreItemRepository _repo;

        public IndexModel(IStoreItemRepository repo)
        {
            _repo = repo;
        }

        public IList<StoreItem> ItemModel { get;set; } = default!;



        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public void OnGet()
        {
            var items = _repo.List(SearchString);
            ItemModel = items.ToList();
        }
    }
}
