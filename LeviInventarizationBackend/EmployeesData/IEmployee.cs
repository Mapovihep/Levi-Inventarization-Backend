using ReactASPCore.Models;

namespace ReactASPCore.EmployeesData
{
    public interface IEmployee
    {
        Task<string> Login(Employee employee, string? token);
        Task<string> Registration(Employee employee);
        Task<string> DeleteEmployee(Guid id, string token);
        Task<string> EmployeesRights(string token);
    }
}
