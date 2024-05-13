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
using Microsoft.AspNetCore.Identity;

namespace UI.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public IndexModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public IList<Order> Order { get; set; } = default!;

        public void OnGet()
        {
            var orders = _repo.ListWithItems();
            Order = orders.ToList();
        }
    }
}
