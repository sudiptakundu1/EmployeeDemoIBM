using EmployeeDemoIBMDb.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace EmployeeDemoIBMDb.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        private readonly ILogger<DatabaseContext> _logger;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger) : base(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);
                new DepartmentsMap(modelBuilder.Entity<Departments>());
                new EmployeesMap(modelBuilder.Entity<Employees>());
            }
            catch (Exception ex)
            {
                var errMsg = "Error while overriding OnModelCreating in DatabaseContext.";
                _logger.LogError(ex, errMsg);

            }
        }
    }
}
