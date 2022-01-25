using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.EmployeesData;
using ReactASPCore.Models;
using ReactASPCore.RoomData;

namespace Inventarization.Controllers
{
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomData _roomData = new RoomData();
        private EmployeeData _employeeData = new EmployeeData();

        [HttpGet]
        [Route("api/rooms")]
        public async Task<IActionResult> GetRooms([FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _roomData.GetRooms());
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpGet]
        [Route("api/rooms/{roomId}")]
        public async Task<IActionResult> GetRoomInventory(Guid roomId, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _roomData.GetRoom(roomId));
            }
            else
            {
                return BadRequest(rights);
            }
        }

        
        [HttpPost]
        [Route("api/rooms")]
        public async Task<IActionResult> AddRoom([FromBody] List<Room> rooms, [FromHeader] string Authorization)
        {
            Console.WriteLine(rooms);
            string rights = await _employeeData.EmployeesRights(Authorization);
            try
            {
                if (rights == "All rights")
                {
                    var response = await _roomData.AddRooms(rooms);
                    return response != null ? Ok(response) : BadRequest("Room was not added - db is not connected");
                }
                else
                {
                    return BadRequest(rights);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(rights);
            }
        }

        [HttpDelete]
        [Route("api/rooms/{roomId}")]
        public async Task<IActionResult> RemoveRoom(Guid roomId, [FromHeader]string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _roomData.RemoveRoom(roomId));
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpPut]
        [Route("api/rooms/{roomId}")]
        public async Task<IActionResult> EditRoom([FromBody] Room room, [FromHeader] string Authorization, Guid roomId)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                var response = await _roomData.EditRoom(room, roomId);
                return response != null ? Ok(response) : NotFound("Room was not found");
            }
            else
            {
                return BadRequest(rights);
            }
        }
    }
}
