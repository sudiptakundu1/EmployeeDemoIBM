using System.ComponentModel.DataAnnotations;

namespace EmployeeDemoIBMDb.EntityFramework.Models
{
    public class EmployeesFullView
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Departments Dept { get; set; }
    }
}
