using EmployeeDemoIBMDb.EntityFramework.Models;
using EmployeeDemoIBMDb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmployeeDemoIBM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private IRepository Repository;
        
        public DepartmentsController(IRepository repository, ILogger<DepartmentsController> logger)
        {
            _logger = logger;
            Repository = repository;
        }

        //[Route("GetAllDepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var depts = await Repository.GetAllDepartments();
                return Ok(depts);
            }
            catch (SqlException sqlExp)
            {
                var errMsg = "Database error occured. Please contact admin : " + sqlExp.Message;
                _logger.LogError(sqlExp, errMsg);
                return NotFound(errMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Route("CreateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> Post(Departments dept)
        {
            try
            {
                bool success = await Repository.AddDepartment(dept);
                return Ok(success);
            }
            catch (SqlException sqlExp)
            {
                var errMsg = "Database error occured. Please contact admin : " + sqlExp.Message;
                _logger.LogError(sqlExp, errMsg);
                return NotFound(errMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Route("UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult> Put(Departments dept)
        {
            try
            {
                bool success = await Repository.UpdateDepartment(dept);
                if (success)
                    return Ok(success);
                else
                    return NotFound("DepartmentId: " + dept.DeptId + " is not found in database.");
            }
            catch (SqlException sqlExp)
            {
                var errMsg = "Database error occured. Please contact admin : " + sqlExp.Message;
                _logger.LogError(sqlExp, errMsg);
                return NotFound(errMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Route("DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int DeptId)
        {
            try
            {
                bool success = await Repository.DeleteDepartment(DeptId);
                if (success)
                    return Ok(success);
                else
                    return NotFound("DepartmentId: " + DeptId + " is not found in database.");

            }
            catch (SqlException sqlExp)
            {
                var errMsg = "Database error occured. Please contact admin : " + sqlExp.Message;
                _logger.LogError(sqlExp, errMsg);
                return NotFound(errMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }
    }
}
