using EmployeeDemoIBMDb.EntityFramework;
using EmployeeDemoIBMDb.EntityFramework.Models;
using EmployeeDemoIBMDb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDemoIBM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private IRepository Repository;

        public EmployeesController(IRepository repository, ILogger<EmployeesController> logger)
        {
            _logger = logger;
            Repository = repository;
        }

        //[Route("GetAllEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var emp = await Repository.GetAllEmployees();
                return Ok(emp);
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

        //[Route("CreateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> Post(Employees emp)
        {
            try
            {
                bool success = await Repository.AddEmployee(emp);
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

        //[Route("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult> Put(Employees emp)
        {
            try
            {
                bool success = await Repository.UpdateEmployee(emp);
                if (success)
                    return Ok(success);
                else
                    return NotFound("EmployeeId: " + emp.Id + " is not found in database.");
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

        //[Route("DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int EmpId)
        {
            try
            {
                bool success = await Repository.DeleteEmployee(EmpId);
                if (success)
                    return Ok(success);
                else
                    return NotFound("EmployeeId: " + EmpId + " is not found in database.");

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
    }
}
