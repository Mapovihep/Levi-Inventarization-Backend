using Microsoft.EntityFrameworkCore;
using ReactASPCore.Models;
using Inventarization.Models;

namespace ReactASPCore.InventorySetupsData
{
    public class InventorySetupsData : IInventorySetups
    {
        public async Task<List<InventorySetup>> GetAllInventorySetups() 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventorySetups = await db.Setups.ToListAsync();
                if (inventorySetups != null)
                {
                    return inventorySetups;
                }
                return null;
            }
        }
        public async Task<InventorySetup> GetInventorySetup(Guid id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventorySetups = await db.Setups.ToListAsync();
                var needed = inventorySetups.Find(x=> x.Id == id);
                if (needed != null)
                {
                    return needed;
                }
                return null;
            }
        }
        public async Task<InventorySetup> AddInventorySetup(InventorySetup inventorySetup)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventorySetups = await db.Setups.ToListAsync();
                if (inventorySetups.Find(x => x.Id == inventorySetup.Id) == null)
                {
                    await db.Setups.AddAsync(inventorySetup);
                    await db.SaveChangesAsync();
                    return inventorySetup;
                }
                return null;
            }
        }
        public async Task<string> RemoveInventorySetup(Guid setupId) {
            string response;
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventorySetups = await db.Setups.ToListAsync();
                var inventorySetup = inventorySetups.Find(x => x.Id == setupId);
                if (inventorySetup != null)
                {
                    db.Setups.Remove(inventorySetup);
                    await db.SaveChangesAsync();
                    response = $"Setup {inventorySetup.Name} with Id - {inventorySetup.Id}  is deleted";
                }
                else
                {
                    response = "Setup is not found";
                }
                return response;
            }
        }
        public async Task<InventorySetup> EditInventorySetup(InventorySetup inventorySetup, Guid setupId) 
        {
            InventorySetup response;
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventorySetups = await db.Setups.ToListAsync();
                var changingInventorySetup = inventorySetups.Find(x => x.Id == setupId);
                inventorySetup.Id = setupId;
                if (changingInventorySetup != null && changingInventorySetup != inventorySetup)
                {
                    db.Setups.Remove(changingInventorySetup);
                    changingInventorySetup = inventorySetup;
                    await db.Setups.AddAsync(changingInventorySetup);
                    await db.SaveChangesAsync();
                    return changingInventorySetup;
                }
                return null;
            }
        }
    }
}
