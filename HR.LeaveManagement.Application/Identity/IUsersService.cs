using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Identity;

public interface IUsersService
{
    Task<List<Employee>> GetEmployees();

    Task<Employee> GetEmployee(string userId);
}
