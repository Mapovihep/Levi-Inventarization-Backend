using Inventarization.Models;
using ReactASPCore.Models;

namespace ReactASPCore.DepartmentData
{
    public interface IDepartment
    {
        Task<List<Department>> GetAllDepartments();
        Task<Department> GetDepartment(Guid departmentId);
        Task<Department> AddDepartment(Department department);
        Task<string> RemoveDepartment(Guid departmentId);
        Task<Department> EditDepartment(Department department, Guid departmentId);

    }
}
