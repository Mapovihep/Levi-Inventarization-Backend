using Microsoft.EntityFrameworkCore;
using ReactASPCore.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace ReactASPCore.InventoryData
{
    public class InventoryData : IInventory
    {
        public async Task<List<Inventory>> GetAllInventory()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventoryLots = await db.InventoryLots.ToListAsync();
                if (inventoryLots != null)
                {
                    return inventoryLots;
                }
            }
            return null;
        }
        public async Task<Inventory> GetInventoryLot(Guid id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventoryLots = await db.InventoryLots.ToListAsync();
                var neededLot = inventoryLots.Find(x => x.Id == id);
                if (neededLot != null)
                {
                    return neededLot;
                }
            }
            return null;
        }
        public async Task<Inventory> AddInventoryLot(Inventory inventoryLot)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventoryLots = await db.InventoryLots.ToListAsync();
                if (inventoryLots.Find(x => x.Id == inventoryLot.Id) == null)
                {
                    await db.InventoryLots.AddAsync(inventoryLot);
                    await db.SaveChangesAsync();
                    return inventoryLot;
                    /*Zen.Barcode.CodeQrBarcodeDraw qrCode = Zen.Barcode.BarcodeDrawFactory.CodeQr;*/
                }
                return null;
            }
        }
        public async Task<string> RemoveInventoryLot(Guid id) {
            string response;
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventoryLots = await db.InventoryLots.ToListAsync();
                var inventoryLot = inventoryLots.Find(x => x.Id == id);
                if (inventoryLot != null)
                {
                    db.InventoryLots.Remove(inventoryLot);
                    await db.SaveChangesAsync();
                    response = $"Lot {inventoryLot.Name} is deleted";
                }
                else
                {
                    response = "Lot is not found";
                }
                return response;
            }
        }
        public async Task<Inventory> EditInventoryLot(Inventory inventoryLot, Guid id) 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var inventoryLots = await db.InventoryLots.ToListAsync();
                var changingInventoryLot = inventoryLots.Find(x => x.Id == id);
                if (changingInventoryLot != null && changingInventoryLot!=inventoryLot)
                {
                    db.InventoryLots.Remove(changingInventoryLot);
                    changingInventoryLot = inventoryLot;
                    await db.InventoryLots.AddAsync(changingInventoryLot);
                    await db.SaveChangesAsync();
                    return inventoryLot;
                }
                return null;
            }
        }
    }
}
