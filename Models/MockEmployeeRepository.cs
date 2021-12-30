using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class MockEmployeeRepository : IEmployeeRepository


    {

        private List<Employee> _employeeList;
            public MockEmployeeRepository()
        {
            _employeeList = new List<Employee> {
            new Employee() {Id=1, Name="Ahmed" ,Email="Ahmed@gmail.com" ,Department=Dept.HR },
            new Employee() {Id=2, Name="Mohammed" ,Email="Mohammed@gmail.com" ,Department=Dept.IT },
            new Employee() { Id = 3, Name = "Mahmoud", Email = "Mahmoud@gmail.com", Department = Dept.IT }

            };    
        }

        public Employee Add(Employee employee)
        {
           employee.Id= _employeeList.Max(e => e.Id )+ 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == Id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;

        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return  _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee employeechanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id ==employeechanges.Id);
            if (employee != null)
            {
                employee.Name = employeechanges.Name;
                employee.Email = employeechanges.Email;
                employee.Department = employeechanges.Department;

            }
            return employee;
        }
    }
}
