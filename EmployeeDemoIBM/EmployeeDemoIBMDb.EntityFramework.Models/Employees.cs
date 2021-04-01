using EmployeeDemoIBMDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
