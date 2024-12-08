using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataServices
{
    public class RolesDAL : IRolesDAL
    {
        private readonly ApplicationDbContext _context;
        public RolesDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.Id == id);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role> CreateRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task DeleteRoleAsync(Role roleToDelete)
        {
            _context.Roles.Remove(roleToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
