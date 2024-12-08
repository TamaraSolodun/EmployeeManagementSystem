using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataServices
{
    public class OfficesDAL : IOfficesDAL
    {
        private readonly ApplicationDbContext _context;
        public OfficesDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Office> GetOfficeAsync(int id, bool includeEployeesData = false)
        {
            var query = _context.Offices.AsQueryable();
            if (includeEployeesData)
            {
                query = query.Include(office => office.Employees).ThenInclude(employee => employee.Role);
            }
            return await query.FirstOrDefaultAsync(office => office.Id == id);
        }

        public async Task<List<Office>> GetOfficesAsync()
        {
            return await _context.Offices.ToListAsync();
        }
        public async Task<Office> CreateOfficeAsync(Office office)
        {
            await _context.Offices.AddAsync(office);
            await _context.SaveChangesAsync();
            return office;
        }

        public async Task<Office> UpdateOfficeAsync(Office office)
        {
            _context.Offices.Update(office);
            await _context.SaveChangesAsync();
            return office;
        }
        public async Task DeleteOfficeAsync(Office officeToDelete)
        {
            _context.Offices.Remove(officeToDelete);
            await _context.SaveChangesAsync();
        }

    }
}
