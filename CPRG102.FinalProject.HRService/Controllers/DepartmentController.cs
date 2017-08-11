using CPRG102.FinalProject.HRService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CPRG102.FinalProject.HRService.Controllers
{
    public class DepartmentController : ApiController
    {
        HRServiceData service = new HRServiceData();

        // GET: api/Department
        public IEnumerable<CPRG102.FinalProject.HRService.Models.Department> Get()
        {
            return service.GetDepartment(); //return departments data JSON
        }

        // GET: api/Department/5
        public CPRG102.FinalProject.HRService.Models.Department Get(int id)
        {
            var dept = service.DepartmentByEmployee(id);
            return dept;
        }

        // POST: api/Department
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Department/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Department/5
        public void Delete(int id)
        {
        }
    }
}
