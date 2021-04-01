﻿using EmployeeDemoIBMDb.EntityFramework.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDemoIBMDb.EntityFramework
{
    public class DepartmentsMap
    {
        public DepartmentsMap(EntityTypeBuilder<Departments> entityBuilder)
        {
            entityBuilder.HasKey(t => t.DeptId);
            entityBuilder.Property(t => t.DeptName).IsRequired();
        }
    }
    
}