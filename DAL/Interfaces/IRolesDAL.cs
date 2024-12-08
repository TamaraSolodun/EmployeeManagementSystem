using DAL.Models;

namespace DAL.Interfaces
{
    public interface IRolesDAL
    {
        Task<Role> GetRoleAsync(int id);
        Task<List<Role>> GetRolesAsync();
        Task<Role> CreateRoleAsync(Role role);
        Task DeleteRoleAsync(Role roleToDelete);
        Task<Role> UpdateRoleAsync(Role role);
    }
}
