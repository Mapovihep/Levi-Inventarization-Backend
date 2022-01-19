using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.EmployeesData;
using ReactASPCore.InventorySetupsData;
using ReactASPCore.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Inventarization.Models;

namespace Inventarization.Controllers
{
    [ApiController]
    public class InventorySetupsController : ControllerBase
    {
        private InventorySetupsData _inventorySetupsData = new InventorySetupsData();
        private EmployeeData _employeeData = new EmployeeData();

        [HttpGet]
        [Route("api/inventorySetups")]
        public async Task<IActionResult> GetAllInventorySetups([FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _inventorySetupsData.GetAllInventorySetups());
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpGet]
        [Route("api/inventorySetups/{setupId}")]
        public async Task<IActionResult> GetInventorySetup(Guid setupId, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _inventorySetupsData.GetInventorySetup(setupId));
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpPost]
        [Route("api/inventorySetups")]
        public async Task<IActionResult> AddInventorySetup([FromBody] InventorySetup inventorySetup, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                var response = await _inventorySetupsData.AddInventorySetup(inventorySetup);
                return response != null ? Ok(response) : BadRequest("This inventory lot was not added - db is not connected");
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpDelete]
        [Route("api/inventorySetups/{setupId}")]
        public async Task<IActionResult> RemoveInventorySetup(Guid setupId, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _inventorySetupsData.RemoveInventorySetup(setupId));
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpPut]
        [Route("api/inventorySetups/{setupId}")]
        public async Task<IActionResult> EditInventoryLot([FromBody] InventorySetup inventorySetup, [FromHeader] string Authorization, Guid setupId)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                var response = await _inventorySetupsData.EditInventorySetup(inventorySetup, setupId);
                return response != null ? Ok(response) : NotFound("This inventory lot was not found");
            }
            else
            {
                return BadRequest(rights);
            }
        }
    }
}
