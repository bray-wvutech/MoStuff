using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models.ShoppingCart;

namespace Data.Repositories.EntityFramework;
public class ShoppingCartRepositoryEf : IShoppingCartRepository
{
    private readonly MoStuffContext _context;

    public ShoppingCartRepositoryEf(MoStuffContext context)
    {
        _context = context;
    }

    public ShoppingCart? GetWithItemsByUserId(string? userId) =>
        _context.ShoppingCarts.Include(c => c.Items)
            .FirstOrDefault(c => c.UserId == userId);

    public void Create(ShoppingCart cart)
    {
        _context.ShoppingCarts.Add(cart);
        _context.SaveChanges();
    }

    public bool Update(ShoppingCart cart)
    {
        _context.Attach(cart).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        return true;
    }

    public void Delete(ShoppingCart cart)
    {
        _context.ShoppingCarts.Remove(cart);
        _context.SaveChanges();
    }

    public int GetItemCountByUserId(string? userId)
    {
        var totalItems = _context.ShoppingCarts
            .Where(cart => cart.UserId == userId)
            .SelectMany(item => item.Items)
            .Sum(sum => sum.Count);
        return totalItems;
    }
}
