using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
