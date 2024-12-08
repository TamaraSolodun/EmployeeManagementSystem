using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? OfficeId { get; set; }
        public Office Office { get; set; }
        public int RoleId { get; set; }
        public Role Role {  get; set; }
    }
}
