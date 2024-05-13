using System.ComponentModel.DataAnnotations;

namespace Domain.Models.ShoppingCart;
public class ShoppingCart
{
    public int Id { get; private set; }

    public string? UserId { get; private set; }

    private List<ShoppingCartItem> _items = new();

    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

    // we really shouldn't have any attributes on domain models
    // see my full comment on the StoreItem model
    [DataType(DataType.Currency)]
    public decimal Total => Items.Sum(i => i.Subtotal);

    public ShoppingCart(string? userId)
    {
        UserId = userId;
    }

    public ShoppingCart()
    {
    }

    public void AddStoreItem(StoreItem storeItem)
    {
        var item = _items.FirstOrDefault(i => i.StoreItemId == storeItem.Id);
        if (item == null)
        {
            _items.Add(new ShoppingCartItem(Id, storeItem));
        }
        else
        {
            item.Count++;
        }
    }

    public bool RemoveItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return false;
        }
        _items.Remove(item);
        return true;
    }

    public bool IncrementItemCount(int id, int count = 1)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return false;
        }
        item.Count += count;
        return true;
    }

    public bool DecrementItemCount(int id, int count = 1)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return false;
        }
        item.Count -= count;
        if (item.Count <= 0)
        {
            _items.Remove(item);
        }
        return true;
    }
}
