using DAL.Models;

namespace DAL.Interfaces
{
    public interface IOfficesDAL
    {
        Task<Office> GetOfficeAsync(int id, bool includeEployeesData = false);
        Task<List<Office>> GetOfficesAsync();
        Task<Office> CreateOfficeAsync(Office office);
        Task DeleteOfficeAsync(Office officeToDelete);
        Task<Office> UpdateOfficeAsync(Office office);
    }
}
