using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain.Models.Order;
using Domain.Interfaces;

namespace UI.Pages.Admin.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public EditModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public int OrderId { get; set; } = 0;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _repo.GetWithItemsById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            OrderId = order.Id;
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

            Order? order = _repo.GetWithItemsById(OrderId);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = Order.Status;
            _repo.Update(order);
            return RedirectToPage("./Index");
        }
    }
}
