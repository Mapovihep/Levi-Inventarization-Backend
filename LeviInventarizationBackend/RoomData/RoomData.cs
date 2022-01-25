using Inventarization.Models;
using Microsoft.EntityFrameworkCore;
using ReactASPCore.Models;

namespace ReactASPCore.RoomData
{
    public class RoomData : IRoom
    {
        public async Task<List<Room>> GetRooms()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                if (rooms != null)
                {
                    return rooms;
                }
                return null;
            }
        }
        public async Task<Room> GetRoom(Guid id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                var room = rooms.Find(x => x.Id == id);
                if (room != null)
                {
                    return room;
                }
                return null;
            }
        }

        public async Task<List<Inventory>> GetRoomInventory(Guid id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                var inventoryLots = await db.InventoryLots.ToListAsync();
                var room = rooms.Find(x => x.Id == id);
                if (room != null)
                {
                    List<Inventory> neededLots = inventoryLots.FindAll(x => x.RoomName == room.Name);
                    return neededLots;
                }
                return null;
            }
        }
        public async Task<List<InventorySetup>> GetRoomInventorySetups(Guid id)
        {
            List<InventorySetup> RoomInventorySetups = new List<InventorySetup>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                var inventorySetups = await db.Setups.ToListAsync();
                var room = rooms.Find(x => x.Id == id);
                if (room != null)
                {
                    List<InventorySetup> neededSetups = inventorySetups.FindAll(x => x.RoomName == room.Name);
                    return neededSetups;
                }
                return null;
            }
        }
        public async Task<List<Room>> AddRooms(List<Room> addedRooms)
        {
            string response;
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                var flag = addedRooms.TrueForAll(addedRoom => rooms.Find(x => x.Id == addedRoom.Id) == null);
                await db.Rooms.AddRangeAsync(addedRooms);
                if (flag)
                {
                    await db.SaveChangesAsync();
                    return addedRooms;
                }                
                return null;
            }
        }

        public async Task<string> RemoveRoom(Guid roomId)
        {
            string response;
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                var room = rooms.Find(x => x.Id == roomId);
                if (room != null)
                {
                    db.Rooms.Remove(room);
                    await db.SaveChangesAsync();
                    response = $"Lot {room.Name} is deleted";
                }
                else
                {
                    response = "Lot is not found";
                }
                return response;
            }
        }

        public async Task<Room> EditRoom(Room room, Guid roomId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var rooms = await db.Rooms.ToListAsync();
                var changingRoom = rooms.Find(x => x.Id == roomId);
                room.Id = roomId;
                if (changingRoom != null && changingRoom.Name != room.Name)
                {
                    db.Rooms.Remove(changingRoom);
                    changingRoom = room;
                    await db.Rooms.AddAsync(changingRoom);
                    await db.SaveChangesAsync();
                    return room;
                }
                return null;
            }
        }
    }
}
