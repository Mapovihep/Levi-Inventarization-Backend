using Inventarization.Models;
using ReactASPCore.Models;

namespace ReactASPCore.RoomData
{
    public interface IRoom
    {
        Task<Room> GetRoom(Guid id);
        Task<List<Room>> GetRooms();
        Task<Room> AddRoom(Room room);
        Task<string> RemoveRoom(Guid roomId);
        Task<Room> EditRoom(Room room, Guid roomId);
    }
}
