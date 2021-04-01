using System.ComponentModel.DataAnnotations;

namespace EmployeeDemoIBMDb.EntityFramework.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DeptId { get; set; }
    }
}
