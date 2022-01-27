using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.InventoryData;
using ReactASPCore.Models;


namespace Inventarization.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private InventoryData _inventoryData = new InventoryData();
        
        [HttpGet]
        [Route("api/inventory")]
        public async Task<IActionResult> GetAllInventory([FromHeader] string Authorization)
        {
            try 
            {
                return Ok(await _inventoryData.GetAllInventory());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/inventory/{id}")]
        public async Task<IActionResult> GetInventoryLot(Guid id, [FromHeader] string Authorization)
        {
            try 
            {
                return Ok(await _inventoryData.GetInventoryLot(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/inventory")]
        public async Task<IActionResult> AddInventoryLot([FromBody] Inventory inventoryLot, [FromHeader] string Authorization)
        {
            try
            {
                var response = await _inventoryData.AddInventoryLot(inventoryLot);
                return response != null ? Ok(response) : BadRequest("Inventory lot was not added - db is not connected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        [HttpDelete]
        [Route("api/inventory/{id}")]
        public async Task<IActionResult> RemoveInventoryLot(Guid id, [FromHeader] string Authorization)
        {
            try 
            {
                return Ok(await _inventoryData.RemoveInventoryLot(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        [Route("api/inventory/{id}")]
        public async Task<IActionResult> EditInventoryLot([FromBody] Inventory inventoryLot, [FromHeader] string Authorization, Guid id)
        {
            try 
            {
                var response = await _inventoryData.EditInventoryLot(inventoryLot, id);
                return response != null ? Ok(response) : NotFound("This inventory lot was not found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
