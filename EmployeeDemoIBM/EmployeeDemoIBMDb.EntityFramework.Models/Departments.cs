using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDemoIBMDb.EntityFramework.Models
{
    public class Departments
    {
        [Key]
        public int DeptId { get; set; }

        [Required]
        public string DeptName { get; set; }
    }
}