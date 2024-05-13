using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Order;
public class Order
{
    public int Id { get; private set; }

    public string? UserId { get; private set; }

    // we really shouldn't have any attributes on domain models
    // see my full comment on the StoreItem model
    // similarly this should really be a private set but we need
    // a public set because we use this as part of the Checkout view model
    // again see the StoreItem model comments as to why we are doing this

    [Display(Name = "Email")]
    public string? UserEmail { get; private set; }

    [Display(Name = "Name")]
    public string? CustomerName { get; set; }

    [Display(Name = "Shipping Address")]
    public string? ShippingAddress { get; set; }

    public string? Status { get; set; } = "Pending";
    
    private List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    [DataType(DataType.Currency)]
    public decimal Total => Items.Sum(i => i.Subtotal);

    public Order()
    {
    }

    public Order(Models.ShoppingCart.ShoppingCart cart, string? email = null, 
        string? customerName = null, string? shippingAddress = null)
    {
        UserEmail = email;
        CustomerName = customerName;
        ShippingAddress = shippingAddress;
        UserId = cart.UserId;
        foreach (var item in cart.Items)
        {
            _items.Add(new OrderItem(Id, item));
        }
    }
}
