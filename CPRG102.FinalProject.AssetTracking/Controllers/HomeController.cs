using CPRG102.FinalProject.AssetTracking.Models;
using CPRG102.FinalProject.AssetTracking.ViewModels;
using CPRG102.FinalProject.BLL;
using CPRG102.FinalProject.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPRG102.FinalProject.AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        HttpClient ClientEmployee;
        HttpClient ClientDepartment;
        string URLemployees = "http://localhost:63353/api/Employee";
        string URLdepartments = "http://localhost:63353/api/Department";

        public HomeController()
        {
            ClientEmployee = new HttpClient();
            ClientEmployee.BaseAddress = new Uri(URLemployees);
            ClientEmployee.DefaultRequestHeaders.Accept.Clear();
            ClientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ClientDepartment = new HttpClient();
            ClientDepartment.BaseAddress = new Uri(URLdepartments);
            ClientDepartment.DefaultRequestHeaders.Accept.Clear();
            ClientDepartment.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage msg = await ClientEmployee.GetAsync(URLemployees);
            
            var employees = new List<Employee>();

            if (msg.IsSuccessStatusCode)
            {
                var data = msg.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            }

            var mainViewModel = new HomePageFilterOptionsViewModel();
            var employeesList = new List<SelectListItem>();
            var assetTypeList = new List<SelectListItem>();

            AssetTypeManager manager = new AssetTypeManager();
            var AssetTypes = manager.GetAssetTypes();

            foreach (var em in employees)
            {
                //for employee drop-down list
                employeesList.Add(new SelectListItem { Value = em.EmployeeNumber.ToString(), Text = $"{em.FirstName} {em.LastName}" });
            }

            foreach(var type in AssetTypes)
            {
                //for asset type drop-down list
                assetTypeList.Add(new SelectListItem { Value = type.Id.ToString(), Text = type.Name });
            }

            mainViewModel.AssetType = assetTypeList;
            mainViewModel.Employees = employeesList;

            return View(mainViewModel);
        }

        public async Task<PartialViewResult> AllAssets()
        {
            HttpResponseMessage msgEmployee = await ClientEmployee.GetAsync(URLemployees);
            var employees = new List<Employee>();
            if (msgEmployee.IsSuccessStatusCode)
            {
                var data = msgEmployee.Content.ReadAsStringAsync().Result;
                //deserialize and convert to List<Employee>
                employees = JsonConvert.DeserializeObject<List<Employee>>(data);//convert from Json to List<Employee> 
            }

            HttpResponseMessage msgDepartment = await ClientDepartment.GetAsync(URLdepartments);
            var departments = new List<Department>();
            if (msgDepartment.IsSuccessStatusCode)
            {
                var data = msgDepartment.Content.ReadAsStringAsync().Result;
                //deserialize and convert to List<Department>
                departments = JsonConvert.DeserializeObject<List<Department>>(data);//convert from Json to List<Department> 
            }

            AssetManager manager = new AssetManager();
            AssetTypeManager typeManager = new AssetTypeManager();
            var AssetsObjectModels = new List<HomePagePartialViewModel>();

            var assets = manager.GetAssets();
            var types = typeManager.GetAssetTypes();

            //Get collection of assigned assets
            var AssignedAssets = (from a in assets
                                      join t in types
                                      on a.AssetTypeId equals t.Id
                                      join e in employees
                                      on a.AssignedTo equals e.EmployeeNumber
                                      join d in departments
                                      on e.DepartmentId equals d.Id
                                      where a.AssignedTo != null
                                      select new HomePagePartialViewModel
                                      {
                                          AssetDescription = a.Description,
                                          AssetTypeName = t.Name,
                                          TagNumber = a.TagNumber,
                                          SerialNumber = a.SerialNumber,
                                          EmployeeName = $"{e.FirstName} {e.LastName}",
                                          DepartmentLocation = d.Location
                                      }).ToList();

            AssetsObjectModels.AddRange(AssignedAssets);
            
            //Get collection of unassigned assets
            var UnassigneddAssets = (from a in assets
                                      join t in types
                                      on a.AssetTypeId equals t.Id
                                      where a.AssignedTo == null
                                      select new HomePagePartialViewModel
                                      {
                                          AssetDescription = a.Description,
                                          AssetTypeName = t.Name,
                                          TagNumber = a.TagNumber,
                                          SerialNumber = a.SerialNumber,
                                          EmployeeName = "-",
                                          DepartmentLocation = "-"
                                      }).ToList();

            AssetsObjectModels.AddRange(UnassigneddAssets);

            return PartialView("_AllAssets", AssetsObjectModels);
        }

        public async Task<PartialViewResult> AssignedAssets()
        {
            HttpResponseMessage msgEmployee = await ClientEmployee.GetAsync(URLemployees);

            var employees = new List<Employee>();
            if (msgEmployee.IsSuccessStatusCode)
            {
                var data = msgEmployee.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            }

            HttpResponseMessage msgDepartment = await ClientDepartment.GetAsync(URLdepartments);
            var departments = new List<Department>();
            if (msgDepartment.IsSuccessStatusCode)
            {
                var data = msgDepartment.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(data); 
            }

            AssetManager manager = new AssetManager();
            AssetTypeManager typeManager = new AssetTypeManager();

            var assets = manager.GetAssets();
            var types = typeManager.GetAssetTypes();

            var AssetsObjectModels = (from a in assets
                                     join t in types
                                     on a.AssetTypeId equals t.Id
                                     join e in employees
                                     on a.AssignedTo equals e.EmployeeNumber
                                     join d in departments
                                     on e.DepartmentId equals d.Id
                                     where a.AssignedTo != null
                                     select new HomePagePartialViewModel
                                     {
                                         AssetDescription = a.Description,
                                         AssetTypeName = t.Name,
                                         TagNumber = a.TagNumber,
                                         SerialNumber = a.SerialNumber,
                                         EmployeeName = $"{e.FirstName} {e.LastName}",
                                         DepartmentLocation = d.Location
                                     }).ToList();

            return PartialView("_AssignedAssets", AssetsObjectModels);
        }

        public PartialViewResult UnassignedAssets()
        {
            AssetManager manager = new AssetManager();
            AssetTypeManager typeManager = new AssetTypeManager();

            var assets = manager.GetAssets();
            var types = typeManager.GetAssetTypes();

            var AssetsObjectModels = (from a in assets
                                      join t in types
                                      on a.AssetTypeId equals t.Id
                                      where a.AssignedTo == null
                                      select new HomePagePartialViewModel
                                      {
                                          AssetDescription = a.Description,
                                          AssetTypeName = t.Name,
                                          TagNumber = a.TagNumber,
                                          SerialNumber = a.SerialNumber,
                                          EmployeeName = "-",
                                          DepartmentLocation = "-"
                                      }).ToList();

            return PartialView("_UnassignedAssets", AssetsObjectModels);
        }

        public async Task<JsonResult> AssetsByEmployee(string name) //string name comes in from Jquery/Ajax method
        {
            HttpResponseMessage msgEmployee = await ClientEmployee.GetAsync(URLemployees);

            var employees = new List<Employee>();
            if (msgEmployee.IsSuccessStatusCode)
            {
                var data = msgEmployee.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            }

            HttpResponseMessage msgDepartment = await ClientDepartment.GetAsync(URLdepartments);
            var departments = new List<Department>();
            if (msgDepartment.IsSuccessStatusCode)
            {
                var data = msgDepartment.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(data);
            }

            AssetManager manager = new AssetManager();
            AssetTypeManager typeManager = new AssetTypeManager();

            var assets = manager.GetAssets();
            var types = typeManager.GetAssetTypes();

            var AssetsObjectModels = (from a in assets
                                      join t in types
                                      on a.AssetTypeId equals t.Id
                                      join e in employees
                                      on a.AssignedTo equals e.EmployeeNumber
                                      join d in departments
                                      on e.DepartmentId equals d.Id
                                      where a.AssignedTo == name
                                      select new HomePagePartialViewModel
                                      {
                                          AssetDescription = a.Description,
                                          AssetTypeName = t.Name,
                                          TagNumber = a.TagNumber,
                                          SerialNumber = a.SerialNumber,
                                          EmployeeName = $"{e.FirstName} {e.LastName}",
                                          DepartmentLocation = d.Location
                                      }).ToList();

            //return Json collection for ajax method (Index view <script> section)
            return Json(AssetsObjectModels, "text/json", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AssetsByType(int id) //id comes in from Jquery/Ajax method
        {
            HttpResponseMessage msgEmployee = await ClientEmployee.GetAsync(URLemployees);

            var employees = new List<Employee>();
            if (msgEmployee.IsSuccessStatusCode)
            {
                var data = msgEmployee.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            }

            HttpResponseMessage msgDepartment = await ClientDepartment.GetAsync(URLdepartments);
            var departments = new List<Department>();
            if (msgDepartment.IsSuccessStatusCode)
            {
                var data = msgDepartment.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(data);
            }

            AssetManager manager = new AssetManager();
            AssetTypeManager typeManager = new AssetTypeManager();
            var AssetsObjectModels = new List<HomePagePartialViewModel>();

            var assets = manager.GetAssets();
            var types = typeManager.GetAssetTypes();

            var AssignedAssets = (from a in assets
                                  join t in types
                                  on a.AssetTypeId equals t.Id
                                  join e in employees
                                  on a.AssignedTo equals e.EmployeeNumber
                                  join d in departments
                                  on e.DepartmentId equals d.Id
                                  where a.AssignedTo != null && t.Id == id
                                  select new HomePagePartialViewModel
                                  {
                                      AssetDescription = a.Description,
                                      AssetTypeName = t.Name,
                                      TagNumber = a.TagNumber,
                                      SerialNumber = a.SerialNumber,
                                      EmployeeName = $"{e.FirstName} {e.LastName}",
                                      DepartmentLocation = d.Location
                                  }).ToList();

            AssetsObjectModels.AddRange(AssignedAssets);

            var UnassigneddAssets = (from a in assets
                                     join t in types
                                     on a.AssetTypeId equals t.Id
                                     where a.AssignedTo == null && t.Id == id
                                     select new HomePagePartialViewModel
                                     {
                                         AssetDescription = a.Description,
                                         AssetTypeName = t.Name,
                                         TagNumber = a.TagNumber,
                                         SerialNumber = a.SerialNumber,
                                         EmployeeName = "-",
                                         DepartmentLocation = "-"
                                     }).ToList();

            AssetsObjectModels.AddRange(UnassigneddAssets);

            //return Json collection for ajax method (Index view <script> section)
            return Json(AssetsObjectModels, "text/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}