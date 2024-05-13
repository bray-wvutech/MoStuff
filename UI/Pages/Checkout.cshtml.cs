using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace UI.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly IShoppingCartRepository _shoppingCartRepo;
        private readonly IOrderRepository _orderRepo;

        public CheckoutModel(IShoppingCartRepository shoppingCartRepo,
            IOrderRepository orderRepo)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _orderRepo = orderRepo;
        }

        [BindProperty]
        public Order OrderModel { get; set; } = default!;

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                ShoppingCart? cart = _shoppingCartRepo.GetWithItemsByUserId(userId);
                if (cart != null)
                {
                    OrderModel = new Order(cart);
                }
            }
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

            ShoppingCart? cart = _shoppingCartRepo.GetWithItemsByUserId(userId);
            if (cart == null)
            {
                return NotFound();
            }

            var email = User.FindFirstValue(ClaimTypes.Email);
            Order order = new(cart, email, OrderModel.CustomerName, 
                OrderModel.ShippingAddress);
            _orderRepo.Update(order);
            _shoppingCartRepo.Delete(cart);
            return RedirectToPage("Success");
        }
    }
}
