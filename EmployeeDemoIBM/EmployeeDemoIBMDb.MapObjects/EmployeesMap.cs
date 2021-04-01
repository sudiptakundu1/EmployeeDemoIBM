using EmployeeDemoIBMDb.EntityFramework.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDemoIBMDb.EntityFramework
{
    public class EmployeesMap
    {
        public EmployeesMap(EntityTypeBuilder<Employees> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.DeptId).IsRequired();
        }
    }
}