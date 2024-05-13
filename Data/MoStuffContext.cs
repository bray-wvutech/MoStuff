using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Models.ShoppingCart;
using Domain.Models.Order;

namespace Data;
public class MoStuffContext : IdentityDbContext
{
    public MoStuffContext(DbContextOptions<MoStuffContext> options) : base(options)
    {
    }

    public DbSet<StoreItem> StoreItems { get; set; } = default!;
    public DbSet<ShoppingCart> ShoppingCarts { get; set; } = default!;
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
}