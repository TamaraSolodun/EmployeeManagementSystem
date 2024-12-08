using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
