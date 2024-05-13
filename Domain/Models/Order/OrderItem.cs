using Domain.Models.ShoppingCart;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Order;
public class OrderItem
{
    public int Id { get; private set; }

    public int OrderId { get; private set; }

    public int StoreItemId { get; private set; }

    // we really shouldn't have any attributes on domain models
    // see my full comment on the StoreItem model
    [StringLength(60, MinimumLength = 1)]
    [Display(Name = "Item")]
    public string? Name { get; private set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; private set; }

    [Display(Name = "Quantity")]
    public int Count { get; private set; }

    [DataType(DataType.Currency)]
    public decimal Subtotal => Price * Count;

    public OrderItem()
    {
    }

    public OrderItem(int orderId, ShoppingCartItem item)
    {
        OrderId = orderId;
        StoreItemId = item.StoreItemId;
        Name = item.Name;
        Price = item.Price;
        Count = item.Count;
    }
}
