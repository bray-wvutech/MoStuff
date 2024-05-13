using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain.Models.Order;
using Domain.Interfaces;

namespace UI.Pages.Admin.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public DeleteModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public Order Order { get; set; } = default!;

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
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _repo.GetWithItemsById(id.Value);
            if (order != null)
            {
                Order = order;
                _repo.Delete(order);
            }

            return RedirectToPage("./Index");
        }
    }
}
