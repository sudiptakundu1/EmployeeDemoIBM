using EmployeeDemoIBMDb.EntityFramework;
using EmployeeDemoIBMDb.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDemoIBMDb.Repository
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        DatabaseContext dbContext;
        public DbSet<Departments> Departments;
        public DbSet<Employees> Employees;

        public Repository(DatabaseContext databaseContext, ILogger<Repository> logger)
        {
            this.dbContext = databaseContext;
            _logger = logger;
            Departments = dbContext.Set<Departments>();
            Employees = dbContext.Set<Employees>();
        }

        public async Task<List<Departments>> GetAllDepartments()
        {
            try
            {
                return await Departments.Where(dept => !String.IsNullOrEmpty(dept.DeptName)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Departments>> GetDepartmentById(int deptId)
        {
            try
            {
                return await Departments.Where(dept => dept.DeptId == deptId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddDepartment(Departments dept)
        {
            dbContext.Entry(dept).State = EntityState.Added;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDepartment(Departments dep)
        {
            var existingDept = Departments.Where(dept => dept.DeptId == dep.DeptId).FirstOrDefault();

            if (existingDept != null)
            {
                existingDept.DeptName = dep.DeptName;

                await dbContext.SaveChangesAsync();
            }
            else
                return false;

            return true;
        }

        public async Task<bool> DeleteDepartment(int deptId)
        {
            var existingDept = Departments.Where(dept => dept.DeptId == deptId).FirstOrDefault();

            if (existingDept != null)
            {
                Departments.Remove(existingDept);
                await dbContext.SaveChangesAsync();
            }
            else
                return false;

            return true;
        }

        public async Task<List<EmployeesFullView>> GetAllEmployees()
        {

            var emps = from emp in Employees.Where(e => !String.IsNullOrEmpty(e.Name))
                       join dept in Departments.Where(dept => !String.IsNullOrEmpty(dept.DeptName))
                       on emp.DeptId equals dept.DeptId
                       select new EmployeesFullView
                       {
                           Id = emp.Id,
                           Name = emp.Name,
                           Dept = new Departments()
                           {
                               DeptId = emp.DeptId,
                               DeptName = dept.DeptName
                           }
                       };

            return await emps.ToListAsync();
        }

        public async Task<bool> AddEmployee(Employees emp)
        {
            dbContext.Entry(emp).State = EntityState.Added;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployee(Employees emp)
        {
            var existingEmp = Employees.Where(e => e.Id == emp.Id).FirstOrDefault();

            if (existingEmp != null)
            {
                existingEmp.Name = emp.Name;
                existingEmp.DeptId = emp.DeptId;

                await dbContext.SaveChangesAsync();
            }
            else
                return false;

            return true;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            var existingEmp = Employees.Where(emp => emp.Id == empId).FirstOrDefault();

            if (existingEmp != null)
            {
                Employees.Remove(existingEmp);
                await dbContext.SaveChangesAsync();
            }
            else
                return false;

            return true;
        }
    }
}
