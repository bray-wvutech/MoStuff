using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.ShoppingCart;
public class ShoppingCartItem
{
    public int Id { get; private set; }

    public int ShoppingCartId { get; private set; }

    public int StoreItemId { get; private set; }

    // we really shouldn't have any attributes on domain models
    // see my full comment on the StoreItem model
    [Display(Name = "Item")]
    public string Name { get; set; } = "";

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public string? PictureUri { get; set; }

    [Display(Name = "Quantity")]
    public int Count { get; set; }

    [DataType(DataType.Currency)]
    public decimal Subtotal => Price * Count;

    public ShoppingCartItem()
    {

    }

    public ShoppingCartItem(int shoppingCartId, StoreItem item)
    {
        ShoppingCartId = shoppingCartId;
        StoreItemId = item.Id;
        Name = item.Name;
        Price = item.Price;
        PictureUri = item.PictureUri;
        Count = 1;
    }
}
