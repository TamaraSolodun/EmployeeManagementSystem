using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace API.Models.EmployeeViewModels
{
    public class CreateEmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        public string Position { get; set; }
        public int? OfficeId { get; set; }
        public int RoleId { get; set; }
    }
}
