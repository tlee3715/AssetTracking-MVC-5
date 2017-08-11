using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPRG102.FinalProject.AssetTracking.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}