using System.ComponentModel.DataAnnotations;

namespace API.Models.EmployeeViewModels
{
    public class CreateEmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        public string Position { get; set; }
        public int? OfficeId { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
