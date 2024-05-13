using Domain.Interfaces;
using Domain.Models;
using Domain.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace UI.Pages
{
    [Authorize]
    public class StoreItemModel : PageModel
    {
        private readonly IStoreItemRepository _storeItemRepo;
        private readonly IShoppingCartRepository _shoppingCartRepo;

        [BindProperty]
        public StoreItem Item { get; set; } = new();

        public StoreItemModel(IStoreItemRepository storeItemRepo, 
            IShoppingCartRepository shoppingCartRepo)
        {
            _storeItemRepo = storeItemRepo;
            _shoppingCartRepo = shoppingCartRepo;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _storeItemRepo.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            Item = item;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            // we might want to take the logic below and move it to a service
            // with an interface and implementation in the domain
            // but I wanted to keep this example simple and focused on the
            // repositories as far as interfaces go

            ShoppingCart? cart = _shoppingCartRepo.GetWithItemsByUserId(userId);

            if (cart == null)
            {
                cart = new ShoppingCart(userId);
                cart.AddStoreItem(Item);
                _shoppingCartRepo.Create(cart);
                return RedirectToPage("Index");
            }

            cart.AddStoreItem(Item);
            _shoppingCartRepo.Update(cart);
            return RedirectToPage("Index");
        }
    }
}
