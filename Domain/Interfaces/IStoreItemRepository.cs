using Domain.Models;

namespace Domain.Interfaces;

public interface IStoreItemRepository
{
    // all of these repository methods would be async
    // in a real-world application
    // but we are doing the simplest version we can
    // to concentrate on the architecture and
    // interface/implementation separation

    IEnumerable<StoreItem> List(string? filter = "");

    StoreItem? GetById(int id);

    void Create(StoreItem item);

    bool Update(StoreItem item);

    void Delete(StoreItem item);
}
