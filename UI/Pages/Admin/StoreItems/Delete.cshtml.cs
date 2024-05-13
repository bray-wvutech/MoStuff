using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Interfaces;
using Domain.Models;

namespace RazorCrudUI.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly IStoreItemRepository _repo;

        public DeleteModel(IStoreItemRepository repo)
        {
            _repo = repo;  
        }

        [BindProperty]
        public StoreItem Item { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _repo.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            Item = item;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _repo.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            _repo.Delete(item);
            return RedirectToPage("Index");
        }
    }
}
