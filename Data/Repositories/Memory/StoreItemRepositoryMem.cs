using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Memory;
public class StoreItemRepositoryMem : IStoreItemRepository
{
    IList<StoreItem> _list;

    public StoreItemRepositoryMem()
    {
        _list = new List<StoreItem>
        {
            new StoreItem { Id = 1, Name = "Item 1", Description = "Description 1", Price = 1.99m },
            new StoreItem { Id = 2, Name = "Item 2", Description = "Description 2", Price = 2.99m },
            new StoreItem { Id = 3, Name = "Item 3", Description = "Description 3", Price = 3.99m },
            new StoreItem { Id = 4, Name = "Item 4", Description = "Description 4", Price = 4.99m },
            new StoreItem { Id = 5, Name = "Item 5", Description = "Description 5", Price = 5.99m }
        };
    }

    public IEnumerable<StoreItem> List(string? filter = "")
    {
        if (string.IsNullOrEmpty(filter))
        {
            return _list.AsEnumerable();
        }
        return _list.Where(i => i.Name.ToLower().Contains(filter.ToLower()));
    }

    public StoreItem? GetById(int id) =>_list.FirstOrDefault(x => x.Id == id);

    public void Create(StoreItem item)
    {
        item.Id = _list.Max(x => x.Id) + 1;
        _list.Add(item);
    }

    public bool Update(StoreItem item)
    {
        var existingItem = _list.FirstOrDefault(x => x.Id == item.Id);
        if (existingItem == null)
            return false;
        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        existingItem.Price = item.Price;
        return true;
    }

    public void Delete(StoreItem item)
    {
        _list.Remove(item);
    }
}
