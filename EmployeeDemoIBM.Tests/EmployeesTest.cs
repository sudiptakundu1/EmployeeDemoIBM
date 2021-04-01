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
    public class EmployeesTest
    {
        EmployeesController _controller;
        IRepository _service;

        public EmployeesTest()
        {
            _service = new EmployeeDemoIBMFakeClass();
            _controller = new EmployeesController(_service, null);
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
            var items = Assert.IsType<List<EmployeesFullView>>(okResult.Value);
            Assert.True(items.Count > 0);
        }
    }
}
