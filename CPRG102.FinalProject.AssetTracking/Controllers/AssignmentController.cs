using CPRG102.FinalProject.AssetTracking.Models;
using CPRG102.FinalProject.AssetTracking.ViewModels;
using CPRG102.FinalProject.BLL;
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
    public class AssignmentController : Controller
    {
        HttpClient ClientEmployee;
        string URLemployees = "http://localhost:63353/api/Employee";

        public AssignmentController()
        {
            ClientEmployee = new HttpClient();
            ClientEmployee.BaseAddress = new Uri(URLemployees);
            ClientEmployee.DefaultRequestHeaders.Accept.Clear();
            ClientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Assignment
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage msg = await ClientEmployee.GetAsync(URLemployees);

            var employees = new List<Employee>();

            if (msg.IsSuccessStatusCode)
            {
                var data = msg.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(data); //employee data
            }

            AssetTypeManager TypeMng = new AssetTypeManager();
            var Types = TypeMng.GetAssetTypes(); //get asset types

            AssetManager AssetMng = new AssetManager();
            var Assets = AssetMng.GetAssets();//get all assets            

            var UnassignedEmployee = employees //Collection of employee who haven't been assigned any asset
                .Where(o => Assets.All(a => o.EmployeeNumber != a.AssignedTo))
                .Select(o => new Employee
                {
                    Id = o.Id,
                    EmployeeNumber = o.EmployeeNumber,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Position = o.Position,
                    Phone = o.Phone,
                    DepartmentId = o.DepartmentId
                }).ToList();

            var assetTypeList = new List<SelectListItem>();

            foreach (var type in Types)
            {
                assetTypeList.Add(new SelectListItem { Value = type.Id.ToString(), Text = type.Name });
            }

            var employeesList = UnassignedEmployee.Select(o => new SelectListItem { Value = o.EmployeeNumber, Text = $"{o.FirstName} {o.LastName} (Phone: {o.Phone})" });

            AssignmentViewModel ViewModel = new AssignmentViewModel();

            ViewModel.AssetType = assetTypeList;
            ViewModel.UnassignedEmployees = employeesList;

            return View(ViewModel);
        }

        public JsonResult GetAssetByType(int id)
        {
            AssetManager manager = new AssetManager();

            var assets = manager.AssetsByType(id).Where(o => o.AssignedTo == null).Select(o => new SelectListItem { Value = o.Id.ToString(), Text = $"{o.Model.Name} ({o.Manufacturer.Name})" });

            return Json(assets, "text/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProcessData(FormCollection formData)
        {
            AssetManager manager = new AssetManager();
            var asset = manager.Find(int.Parse(formData["Assets"])); //get asset by selected drop down list
            asset.AssignedTo = formData["Employees"];

            manager.UpdateAsset(asset); //update asset information (method from AssetManager)

            return RedirectToAction("Index");
        }
    }
}