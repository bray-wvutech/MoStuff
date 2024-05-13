using Domain.Models.ShoppingCart;

namespace Domain.Interfaces;
public interface IShoppingCartRepository
{
    // all of these repository methods would be async
    // in a real-world application
    // but we are doing the simplest version we can
    // to concentrate on the architecture and
    // interface/implementation separation

    ShoppingCart? GetWithItemsByUserId(string? userId);

    void Create(ShoppingCart cart);

    bool Update(ShoppingCart cart);

    void Delete(ShoppingCart cart);

    int GetItemCountByUserId(string? userId);
}
