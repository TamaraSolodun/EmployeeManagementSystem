using System.ComponentModel.DataAnnotations;

namespace API.Models.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Position { get; set; }
        public int? OfficeId { get; set; }
        public string? OfficeTitle { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
        public List<string> RoleTitles { get; set; } = new List<string>();
    }
}
