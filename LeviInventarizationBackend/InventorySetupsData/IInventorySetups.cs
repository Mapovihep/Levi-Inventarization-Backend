using Inventarization.Models;
using ReactASPCore.Models;

namespace ReactASPCore.InventorySetupsData
{
    public interface IInventorySetups
    {
        Task<List<InventorySetup>> GetAllInventorySetups();
        Task<InventorySetup> GetInventorySetup(Guid id);
        Task<InventorySetup> AddInventorySetup(InventorySetup inventorySetup);
        Task<string> RemoveInventorySetup(Guid setupId);
        Task<InventorySetup> EditInventorySetup(InventorySetup inventorySetup, Guid setupId);

    }
}
