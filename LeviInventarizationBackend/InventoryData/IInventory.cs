using ReactASPCore.Models;

namespace ReactASPCore.InventoryData
{
    public interface IInventory
    {
        Task<List<Inventory>> GetAllInventory();
        Task<Inventory> GetInventoryLot(Guid id);
        Task<Inventory> AddInventoryLot(Inventory inventoryLot);
        Task<string> RemoveInventoryLot(Guid id);
        Task<Inventory> EditInventoryLot(Inventory inventoryLot, Guid id);

    }
}
