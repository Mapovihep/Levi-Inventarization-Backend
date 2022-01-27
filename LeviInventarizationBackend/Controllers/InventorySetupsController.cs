using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.InventorySetupsData;
using Inventarization.Models;

namespace Inventarization.Controllers
{
    [ApiController]
    public class InventorySetupsController : ControllerBase
    {
        private InventorySetupsData _inventorySetupsData = new InventorySetupsData();

        [HttpGet]
        [Route("api/inventorySetups")]
        public async Task<IActionResult> GetAllInventorySetups([FromHeader] string Authorization)
        {
            try
            {
                return Ok(await _inventorySetupsData.GetAllInventorySetups());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/inventorySetups/{setupId}")]
        public async Task<IActionResult> GetInventorySetup(Guid setupId, [FromHeader] string Authorization)
        {
            try 
            {
                return Ok(await _inventorySetupsData.GetInventorySetup(setupId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/inventorySetups")]
        public async Task<IActionResult> AddInventorySetup([FromBody] InventorySetup inventorySetup, [FromHeader] string Authorization)
        {
            try 
            {
                var response = await _inventorySetupsData.AddInventorySetup(inventorySetup);
                return response != null ? Ok(response) : BadRequest("This inventory lot was not added - db is not connected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("api/inventorySetups/{setupId}")]
        public async Task<IActionResult> RemoveInventorySetup(Guid setupId, [FromHeader] string Authorization)
        {
            try 
            {
                return Ok(await _inventorySetupsData.RemoveInventorySetup(setupId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("api/inventorySetups/{setupId}")]
        public async Task<IActionResult> EditInventoryLot([FromBody] InventorySetup inventorySetup, [FromHeader] string Authorization, Guid setupId)
        {
            try 
            {
                var response = await _inventorySetupsData.EditInventorySetup(inventorySetup, setupId);
                return response != null ? Ok(response) : NotFound("This inventory lot was not found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
