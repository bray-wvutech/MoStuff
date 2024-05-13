using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Interfaces;
using Domain.Models;
using UI.Utilities;

namespace RazorCrudUI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly IStoreItemRepository _repo;
        private readonly IWebHostEnvironment _env;

        public EditModel(IStoreItemRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [BindProperty]
        public StoreItem ItemModel { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var itemmodel =  _repo.GetById(id.Value);
            if (itemmodel == null)
            {
                return NotFound();
            }
            ItemModel = itemmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                ItemModel.PictureUri = PictureHelper.UploadNewImage(_env,
                                       HttpContext.Request.Form.Files[0]);
            }

            if (!_repo.Update(ItemModel))
            { 
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
