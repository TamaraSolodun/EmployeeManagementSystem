using DAL.Models;

namespace DAL.Interfaces
{
    public interface IEmployeesDAL
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<List<Employee>> GetEmployeesAsync(int? officeId = null);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employeeToDelete);
        Task<Employee> UpdateEmployeeAsync(Employee employee);

    }
}
