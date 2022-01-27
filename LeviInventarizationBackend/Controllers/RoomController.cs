using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.EmployeesData;
using ReactASPCore.Models;
using ReactASPCore.RoomData;

namespace Inventarization.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Authorize(Policy = "Bearer")]
    [EnableCors("CorsPolicy")]

    public class RoomController : ControllerBase
    {
        private RoomData _roomData = new RoomData();

        [HttpGet]
        [Route("api/rooms")]
        public async Task<IActionResult> GetRooms([FromHeader] string Authorization)
        {
            try 
            {
                return Ok(await _roomData.GetRooms());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/rooms/{roomId}")]
        [Authorize]
        public async Task<IActionResult> GetRoomInventory(Guid roomId, [FromHeader] string Authorization)
        {
            try
            {
                return Ok(await _roomData.GetRoom(roomId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("testGet")]
        public async Task<IActionResult> GetTest()
        {
            return Ok("Hrllo");
        }




        [HttpPost]
        [Route("api/rooms")]
        public async Task<IActionResult> AddRooms([FromBody] List<Room> rooms, [FromHeader] string Authorization)
        {
            rooms.ForEach(x => 
                Console.WriteLine(x.Name + ' ')
            );
            try
            {
                var response = await _roomData.AddRooms(rooms);
                return response != null ? Ok(response) : BadRequest("Room was not added - db is not connected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("api/rooms/{roomId}")]
        public async Task<IActionResult> RemoveRoom(Guid roomId, [FromHeader]string Authorization)
        {
            try
            {
                return Ok(await _roomData.RemoveRoom(roomId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("api/rooms/{roomId}")]
        public async Task<IActionResult> EditRoom([FromBody] Room room, [FromHeader] string Authorization, Guid roomId)
        {
            try 
            {
                var response = await _roomData.EditRoom(room, roomId);
                return response != null ? Ok(response) : NotFound("Room was not found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
