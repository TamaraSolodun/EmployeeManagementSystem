using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataServices
{
    public class EmployeesDAL : IEmployeesDAL
    {
        private readonly ApplicationDbContext _context;
        public EmployeesDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _context.Employees
                .Include(employee => employee.Office)
                 .Include(employee => employee.EmployeeRoles)
                    .ThenInclude(er => er.Role)
                .FirstOrDefaultAsync(employee => employee.Id == id);
        }


        public async Task<List<Employee>> GetEmployeesAsync(int? officeId = null)
        {
            return await _context.Employees
                .Where(employee => (!officeId.HasValue || employee.OfficeId == officeId))
                .Include(employee => employee.EmployeeRoles)
                    .ThenInclude(er => er.Role)
                .Include(employee => employee.Office)
                .ToListAsync();
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task DeleteEmployeeAsync(Employee employeeToDelete)
        {
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
