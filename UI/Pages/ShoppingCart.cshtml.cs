using Domain.Interfaces;
using Domain.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace UI.Pages
{
    [Authorize]
    public class ShoppingCartModel : PageModel
    {
        private readonly IShoppingCartRepository _repo;

        public ShoppingCart Cart { get; set; } = default!;

        public ShoppingCartModel(IShoppingCartRepository shoppingCartRepo)
        {
            _repo = shoppingCartRepo;
        }

        public void OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                ShoppingCart? cart = _repo.GetWithItemsByUserId(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart(userId);
                    _repo.Create(cart);
                }

                // again we might want to take the logic from all these handlers
                // and move it to a service with an interface and implementation
                // in the domain but I wanted to keep this example simple and
                // focused on the repositories as far as interfaces go

                Cart = cart;
            }
        }

        public void OnPostPlus(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ShoppingCart? cart = _repo.GetWithItemsByUserId(userId);

            if (cart != null)
            {
                if (cart.IncrementItemCount(id))
                {
                    _repo.Update(cart);
                }
                Cart = cart;
            }
        }

        public void OnPostMinus(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ShoppingCart? cart = _repo.GetWithItemsByUserId(userId);

            if (cart != null)
            {
                if (cart.DecrementItemCount(id))
                {
                    _repo.Update(cart);
                }
                Cart = cart;
            }
        }

        public void OnPostRemove(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ShoppingCart? cart = _repo.GetWithItemsByUserId(userId);

            if (cart != null)
            {
                if (cart.RemoveItem(id))
                {
                    _repo.Update(cart);
                }
                Cart = cart;
            }
        }
    }
}
