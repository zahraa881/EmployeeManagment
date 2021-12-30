﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository

    {
        private readonly AppDbContext context;
        private readonly ILogger logger;

        public SqlEmployeeRepository(AppDbContext context ,ILogger<SqlEmployeeRepository> logger)
        {
           this.context = context;
            this.logger = logger;
        }

        public AppDbContext Context { get; }

        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee= context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;

        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            logger.LogInformation("Information log");
            logger.LogError("Error log");
            logger.LogDebug("Debug log");
            logger.LogWarning("Warning log");
            logger.LogTrace("Trace log");
            logger.LogCritical("Critical log");
            return context.Employees.Find(Id);
        }

        public Employee Update(Employee employeechanges)
        {
            var employee = context.Attach(employeechanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeechanges;
        }
    }
}
