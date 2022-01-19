using Microsoft.EntityFrameworkCore;
using ReactASPCore.Models;

namespace ReactASPCore.EmployeesData
{
    public class EmployeeData : IEmployee
    {
        public async Task<string> Registration(Employee employee)
        {
            string response = "User can't be added";
            using (ApplicationContext db = new ApplicationContext())
            { 
                var employees = await db.Employees.ToListAsync();
                if (employees.Find(x => x.Email == employee.Email) == null)
                {
                    employee.UpdatedAt = DateTime.Now;
                    await db.Employees.AddAsync(employee);
                    await db.SaveChangesAsync();
                    response = "Registration is successed";
                }
                else
                {
                    response = "User is found - just sign in";
                }
            }
            return response;
        }
        public async Task<string> Login(Employee employee, string? token)
        {
            string response = "User can't be added";
            using (ApplicationContext db = new ApplicationContext())
            {
                var employees = await db.Employees.ToListAsync();
                var currentEmployee = employees.Find(x => x.Email == employee.Email
                && x.Password == employee.Password);
                if (currentEmployee != null)
                {
                    if (token != "")
                    {
                        Employee withToken = new Employee()
                        {
                            Id = currentEmployee.Id,
                            Email = currentEmployee.Email,
                            Password = currentEmployee.Password,
                            FirstName = currentEmployee.FirstName,
                            LastName = currentEmployee.LastName,
                            Phone = currentEmployee.Phone,
                            IsAdmin = currentEmployee.IsAdmin,
                            UpdatedAt = currentEmployee.UpdatedAt,
                            UpdatedBy = currentEmployee.UpdatedBy,
                            token = token
                        };
                        db.Remove(currentEmployee);
                        await db.AddAsync(withToken);
                        await db.SaveChangesAsync();
                    }
                    response = "Login is successed";
                }
                else
                {
                    response = "User is not found - just sign up";
                }
            }
            return response;
        }
        public async Task<string> DeleteEmployee(Guid id, string token)
        {
            string response = "DB wasn't opened";
            using (ApplicationContext db = new ApplicationContext())
            {
                var employees = await db.Employees.ToListAsync();
                var admin = employees.Find(x => x.token == token);
                var currentEmployee = employees.Find(x => x.Id == id);
                response = (currentEmployee != null && admin.IsAdmin) ? 
                    $"Successfully deleted employee {currentEmployee.Id}"
                    : "User wasn't found or you aren't admin";
                db.Remove(currentEmployee);
                await db.SaveChangesAsync();
            }
            return response;
        }
        public async Task<string> EmployeesRights(string token)
        {
            string response = "All rights";
            using (ApplicationContext db = new ApplicationContext())
            {
                var employees = await db.Employees.ToListAsync();
                var admin = employees.Find(x => x.token == token);
                if(admin != null)
                {
                    response = admin.IsAdmin ? response : "You are not admin";
                }
                else
                {
                    response = "User doesn't exist. Please sign in.";
                }
            }
            return response;
        }
    }
}
