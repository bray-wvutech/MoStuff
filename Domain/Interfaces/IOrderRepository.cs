using Domain.Models.Order;
using Domain.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IOrderRepository
{
    // all of these repository methods would be async
    // in a real-world application
    // but we are doing the simplest version we can
    // to concentrate on the architecture and
    // interface/implementation separation

    IEnumerable<Order> ListWithItems();

    Order? GetWithItemsById(int id);

    void Create(Order order);

    bool Update(Order order);

    void Delete(Order order);
}
