using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
