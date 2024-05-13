using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Interfaces;
using Domain.Models;

namespace RazorCrudUI.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly IStoreItemRepository _repo;

        public DetailsModel(IStoreItemRepository repo)
        {
            _repo = repo;
        }

        public StoreItem ItemModel { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = _repo.GetById(id.Value);
            if (itemmodel == null)
            {
                return NotFound();
            }
            ItemModel = itemmodel;
            return Page();
        }
    }
}
