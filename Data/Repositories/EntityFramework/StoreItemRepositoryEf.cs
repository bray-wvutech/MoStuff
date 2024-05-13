using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.EntityFramework;

public class StoreItemRepositoryEf : IStoreItemRepository
{
    private readonly MoStuffContext _context;

    public StoreItemRepositoryEf(MoStuffContext context)
    {
        _context = context;
    }

    public IEnumerable<StoreItem> List(string? filter = "")
    {
        if (string.IsNullOrEmpty(filter))
        {
            return _context.StoreItems.ToList();
        }
        return _context.StoreItems.Where(i => i.Name.ToLower().Contains(
            filter.ToLower())).OrderBy(i => i.Name).ToList();
    }

    public StoreItem? GetById(int id) =>
        _context.StoreItems.FirstOrDefault(i => i.Id == id);

    public void Create(StoreItem item)
    {
        _context.StoreItems.Add(item);
        _context.SaveChanges();
    }

    public bool Update(StoreItem item)
    {
        _context.Attach(item).State = EntityState.Modified;
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

    public void Delete(StoreItem item)
    {
        _context.StoreItems.Remove(item);
        _context.SaveChanges();
    }
}
