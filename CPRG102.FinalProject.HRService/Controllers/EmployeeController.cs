using CPRG102.FinalProject.HRService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CPRG102.FinalProject.HRService.Controllers
{
    public class EmployeeController : ApiController
    {
        HRServiceData service = new HRServiceData();

        // GET: api/Employee
        public IEnumerable<CPRG102.FinalProject.HRService.Models.Employee> Get()
        {
            return service.GetEmployee(); //return employees data JSON
        }

        // GET: api/Employee/5
        public CPRG102.FinalProject.HRService.Models.Employee Get(int id)
        {
            return service.EmployeeById(id);
        }

        // POST: api/Employee
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }
    }
}
