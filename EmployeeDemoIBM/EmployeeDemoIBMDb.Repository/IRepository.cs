using EmployeeDemoIBMDb.EntityFramework;
using EmployeeDemoIBMDb.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDemoIBMDb.Repository
{
    public interface IRepository
    {
        public Task<List<Departments>> GetAllDepartments();
        public Task<bool> AddDepartment(Departments dept);
        public Task<bool> UpdateDepartment(Departments dep);
        public Task<bool> DeleteDepartment(int deptId);

        public Task<List<EmployeesFullView>> GetAllEmployees();
        public Task<bool> AddEmployee(Employees emp);
        public Task<bool> UpdateEmployee(Employees emp);
        public Task<bool> DeleteEmployee(int empId);
    }
}
