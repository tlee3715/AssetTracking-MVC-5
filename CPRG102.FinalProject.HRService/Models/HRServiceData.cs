using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPRG102.FinalProject.HRService.Models
{
    public class HRServiceData
    {
        HREntities db = new HREntities();

        public IEnumerable<Employee> GetEmployee()
        {
            var employess = db.Employees.Select(o => new Employee { Id = o.Id, EmployeeNumber = o.EmployeeNumber,
                                                                    FirstName = o.FirstName, LastName = o.LastName,
                                                                    Position = o.Position, Phone = o.Phone, DepartmentId = o.DepartmentId }).ToList();
            
            return employess;
        }
        
        public IEnumerable<Department> GetDepartment()
        {
            var depts = db.Departments.Select(o => new Department { Id = o.Id, Name = o.Name, Location = o.Location }).ToList();

            return depts;
        }

        public Department DepartmentByEmployee(int Id)
        {
            var dept = GetDepartment().SingleOrDefault(o => o.Id == Id);

            return dept;
        }

        public Employee EmployeeById(int Id)
        {
            var emp = GetEmployee().SingleOrDefault(o => o.Id == Id);

            return emp;
        }
    }
}