using Domain.Interfaces;
using Domain.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.EntityFramework;
public class OrderRepositoryEf : IOrderRepository
{
    private readonly MoStuffContext _context;

    public OrderRepositoryEf(MoStuffContext context)
    {
        _context = context;
    }

    public void Create(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void Delete(Order order)
    {
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }

    public Order? GetWithItemsById(int id) =>
        _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);

    public IEnumerable<Order> ListWithItems() => 
        _context.Orders.Include(o => o.Items).OrderBy(o => o.Status).ToList();

    public bool Update(Order order)
    {
        try
        {
            _context.Update(order);
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }
}
