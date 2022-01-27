using Inventarization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactASPCore.DepartmentData;
using ReactASPCore.EmployeesData;

namespace Inventarization.Controllers
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private DepartmentData _departmentData = new DepartmentData();
        private EmployeeData _employeeData = new EmployeeData();

        [HttpGet]
        [Route("api/departments")]
        public async Task<IActionResult> GetRooms([FromHeader] string Authorization)
        {
            try
            {
                return Ok(await _departmentData.GetAllDepartments());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/departments/{departmentId}")]
        public async Task<IActionResult> GetRoomInventory(Guid departmentId, [FromHeader] string Authorization)
        {
            try
            {
                return Ok(await _departmentData.GetDepartment(departmentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("api/departments")]
        public async Task<IActionResult> AddRoom([FromBody] Department department, [FromHeader] string Authorization)
        {
            try
            {
                var response = await _departmentData.AddDepartment(department);
                return response != null ? Ok(response) : BadRequest("Department was not added - db is not connected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("api/departments/{departmentId}")]
        public async Task<IActionResult> RemoveRoom(Guid departmentId, [FromHeader] string Authorization)
        {
            try
            {
                return Ok(await _departmentData.RemoveDepartment(departmentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("api/departments/{departmentId}")]
        public async Task<IActionResult> EditRoom([FromBody] Department department, [FromHeader] string Authorization, Guid roomId)
        {
            try
            {
                var response = await _departmentData.EditDepartment(department, roomId);
                return response != null ? Ok(response) : NotFound("This inventory lot was not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
