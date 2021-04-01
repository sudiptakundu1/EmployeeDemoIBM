using EmployeeDemoIBMDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
