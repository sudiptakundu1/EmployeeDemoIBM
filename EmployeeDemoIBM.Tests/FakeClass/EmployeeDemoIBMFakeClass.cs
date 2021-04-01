using EmployeeDemoIBMDb.EntityFramework.Models;
using EmployeeDemoIBMDb.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemoIBM.Tests.FakeClass
{
    public class EmployeeDemoIBMFakeClass : IRepository
    {
        private readonly List<EmployeesFullView> _employees;
        private readonly List<Departments> _departments;
        public EmployeeDemoIBMFakeClass()
        {
            _departments = new List<Departments>()
            {
                new Departments () { DeptId = 1, DeptName = "FakeDepartment1"},
                new Departments() { DeptId = 1, DeptName = "FakeDepartment2" },
                new Departments() { DeptId = 1, DeptName = "FakeDepartment3" },
                new Departments() { DeptId = 1, DeptName = "FakeDepartment4" }
            };

            _employees = new List<EmployeesFullView>()
            {
                new EmployeesFullView () {
                    Id = 1,
                    Name = "FakeEmp1",
                    Dept = new Departments()
                        {
                            DeptId = 1,
                            DeptName = "FakeDepartment1"
                    }
                },
                new EmployeesFullView () {
                    Id = 1,
                    Name = "FakeEmp2",
                    Dept = new Departments()
                        {
                            DeptId = 2,
                            DeptName = "FakeDepartment2"
                    }
                }
            };
        }

        async Task<List<Departments>> IRepository.GetAllDepartments()
        {
            return await Task.Run(() => _departments);
        }

        Task<bool> IRepository.AddDepartment(Departments dept)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository.UpdateDepartment(Departments dep)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository.DeleteDepartment(int deptId)
        {
            throw new NotImplementedException();
        }

        async Task<List<EmployeesFullView>> IRepository.GetAllEmployees()
        {
            return await Task.Run(() => _employees);
        }

        Task<bool> IRepository.AddEmployee(Employees emp)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository.UpdateEmployee(Employees emp)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository.DeleteEmployee(int empId)
        {
            throw new NotImplementedException();
        }
    }
}