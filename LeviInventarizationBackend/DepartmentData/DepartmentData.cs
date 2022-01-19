using Microsoft.EntityFrameworkCore;
using Inventarization.Models;

namespace ReactASPCore.DepartmentData
{
    public class DepartmentData : IDepartment
    {
        public async Task<List<Department>> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var departments = await db.Departments.ToListAsync();
                if (departments != null)
                {
                    return departments;
                }
                else
                {
                    return new List<Department>();
                }
            }
        }
        public async Task<Department> GetDepartment(Guid departmentId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var departments = await db.Departments.ToListAsync();
                var department = departments.Find(x=>x.Id == departmentId);
                if (department != null)
                {
                    return department;
                }
                else
                {
                    return new Department();
                }
            }
        }
        public async Task<Department> AddDepartment(Department department)
        {
            string response = "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var departments = await db.Departments.ToListAsync();
                if (departments.Find(x => x.Id == department.Id) == null)
                {
                    await db.Departments.AddAsync(department);
                    await db.SaveChangesAsync();
                    return department;
                }
                return null;
            }
        }

        public async Task<string> RemoveDepartment(Guid departmentId) {
            string response;
            using (ApplicationContext db = new ApplicationContext())
            {
                var departments = await db.Departments.ToListAsync();
                var department = departments.Find(x => x.Id == departmentId);
                if (department != null)
                {
                    db.Departments.Remove(department);
                    await db.SaveChangesAsync();
                    response = $"Lot {department.Name} is deleted";
                }
                else
                {
                    response = "Lot is not deleted - change request";
                }
                return response;
            }
        }
        public async Task<Department> EditDepartment(Department department, Guid departmentId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var departments = await db.Departments.ToListAsync();
                var changingDepartment = departments.Find(x => x.Id == departmentId);
                if (changingDepartment != null)
                {
                    db.Departments.Remove(changingDepartment);
                    changingDepartment = department;
                    await db.Departments.AddAsync(changingDepartment);
                    await db.SaveChangesAsync();
                    return changingDepartment;
                }
                return null;
            }
        }
    }
}
