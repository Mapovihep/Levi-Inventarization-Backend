using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.EmployeesData;
using ReactASPCore.InventoryData;
using ReactASPCore.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Inventarization.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private InventoryData _inventoryData = new InventoryData();
        private EmployeeData _employeeData = new EmployeeData();
        
        [HttpGet]
        [Route("api/inventory")]
        public async Task<IActionResult> GetAllInventory([FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _inventoryData.GetAllInventory());
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpGet]
        [Route("api/inventory/{id}")]
        public async Task<IActionResult> GetInventoryLot(Guid id, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _inventoryData.GetInventoryLot(id));
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpPost]
        [Route("api/inventory")]
        public async Task<IActionResult> AddInventoryLot([FromBody] Inventory inventoryLot, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            try
            {
                /*BarcodeWriter writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Width = 200,
                        Height = 200,
                        CharacterSet = "UTF-8"
                    },
                    Renderer = new BitmapRenderer()
                };
                Image pngshka = writer.Write(inventoryLot.Name);*/
                /*using (MemoryStream ms = new MemoryStream())
                { 
                    pngshka.Save(ms, ImageFormat.Png);
                }*/
                if (rights == "All rights")
                {
                    var response = await _inventoryData.AddInventoryLot(inventoryLot);
                    return response != null ? Ok(response) : BadRequest("Inventory lot was not added - db is not connected");
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
        [Route("api/inventory/{id}")]
        public async Task<IActionResult> RemoveInventoryLot(Guid id, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _inventoryData.RemoveInventoryLot(id));
            }
            else
            {
                return BadRequest(rights);
            }
        }
        [HttpPut]
        [Route("api/inventory/{id}")]
        public async Task<IActionResult> EditInventoryLot([FromBody] Inventory inventoryLot, [FromHeader] string Authorization, Guid id)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                var response = await _inventoryData.EditInventoryLot(inventoryLot, id);
                return response != null ? Ok(response) : NotFound("This inventory lot was not found");
            }
            else
            {
                return BadRequest(rights);
            }
        }
    }
}
