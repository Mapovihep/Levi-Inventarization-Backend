using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using ReactASPCore.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReactASPCore.EmployeesData
{
    public class EmployeeData : IEmployee
    {
        public async Task<Employee> Registration(Employee employee)
        {
            using (ApplicationContext db = new ApplicationContext())
            { 
                var employees = await db.Employees.ToListAsync();
                if (employees.Find(x => x.Email == employee.Email) == null)
                {
                    byte[] data = new UTF8Encoding().GetBytes(employee.Password);
                    byte[] hashedPassword;
                    SHA256 shaM = new SHA256Managed();
                    hashedPassword = shaM.ComputeHash(data);
                    employee.Password = Convert.ToBase64String(hashedPassword);
                    employee.UpdatedAt = DateTime.Now;
                    await db.Employees.AddAsync(employee);
                    await db.SaveChangesAsync();
                    return employee;
                }
            }
            return null;
        }
        public async Task<Employee> Login(Employee employee)
        {
            byte[] data = new UTF8Encoding().GetBytes(employee.Password);
            byte[] hashedPassword;
            SHA256 shaM = new SHA256Managed();
            hashedPassword = shaM.ComputeHash(data);
            employee.Password = Convert.ToBase64String(hashedPassword);

            using (ApplicationContext db = new ApplicationContext())
            {
                var employees = await db.Employees.ToListAsync();
                var currentEmployee = employees.Find(x => x.Email == employee.Email
                && x.Password == employee.Password);
                if (currentEmployee != null)
                {
                    return currentEmployee;
                }
            }
            return null;
        }
        /*public async Task<string> DeleteEmployee(Guid id, string token)
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
        }*/
    }
}
