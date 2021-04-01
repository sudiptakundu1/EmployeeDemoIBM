using EmployeeDemoIBM.Controllers;
using EmployeeDemoIBM.Tests.FakeClass;
using EmployeeDemoIBMDb.EntityFramework.Models;
using EmployeeDemoIBMDb.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmployeeDemoIBM.Tests
{
    public class DepartmentsTest
    {
        DepartmentsController _controller;
        IRepository _service;

        public DepartmentsTest()
        {
            _service = new EmployeeDemoIBMFakeClass();
            _controller = new DepartmentsController(_service, null);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsSomeItems()
        {
            //Act
            var okResult = _controller.Get().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Departments>>(okResult.Value);
            Assert.True(items.Count > 0);
        }
    }
}
