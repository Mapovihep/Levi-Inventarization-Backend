using ReactASPCore.Models;

namespace ReactASPCore.EmployeesData
{
    public interface IEmployee
    {
        Task<Employee> Login(Employee employee);
        Task<Employee> Registration(Employee employee);
        /*Task<string> DeleteEmployee(Guid id, string token);
        Task<string> EmployeesRights(string token);*/
    }
}
