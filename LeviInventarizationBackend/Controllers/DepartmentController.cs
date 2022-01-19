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
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _departmentData.GetAllDepartments());
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpGet]
        [Route("api/departments/{departmentId}")]
        public async Task<IActionResult> GetRoomInventory(Guid departmentId, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _departmentData.GetDepartment(departmentId));
            }
            else
            {
                return BadRequest(rights);
            }
        }


        [HttpPost]
        [Route("api/departments")]
        public async Task<IActionResult> AddRoom([FromBody] Department department, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                var response = await _departmentData.AddDepartment(department);
                return response != null ? Ok(response) : BadRequest("Department was not added - db is not connected");
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpDelete]
        [Route("api/departments/{departmentId}")]
        public async Task<IActionResult> RemoveRoom(Guid departmentId, [FromHeader] string Authorization)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                return Ok(await _departmentData.RemoveDepartment(departmentId));
            }
            else
            {
                return BadRequest(rights);
            }
        }

        [HttpPut]
        [Route("api/departments/{departmentId}")]
        public async Task<IActionResult> EditRoom([FromBody] Department department, [FromHeader] string Authorization, Guid roomId)
        {
            string rights = await _employeeData.EmployeesRights(Authorization);
            if (rights == "All rights")
            {
                var response = await _departmentData.EditDepartment(department, roomId);
                return response != null ? Ok(response) : NotFound("This inventory lot was not found");
            }
            else
            {
                return BadRequest(rights);
            }
        }
    }
}
