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
    public class NewAssetController : Controller
    {
        HttpClient ClientEmployee;
        string URLemployees = "http://localhost:63353/api/Employee";

        public NewAssetController()
        {
            ClientEmployee = new HttpClient();
            ClientEmployee.BaseAddress = new Uri(URLemployees);
            ClientEmployee.DefaultRequestHeaders.Accept.Clear();
            ClientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        // GET: NewAsset
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage msg = await ClientEmployee.GetAsync(URLemployees);

            var employees = new List<Employee>();

            if (msg.IsSuccessStatusCode)
            {
                var data = msg.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            }

            AssetTypeManager TypeMng = new AssetTypeManager();
            ManufacturerManager ManufacturerMng = new ManufacturerManager();
            ModelManager ModelMng = new ModelManager();
            var Types = TypeMng.GetAssetTypes();
            var Manufaturers = ManufacturerMng.GetManufacturers();
            var Models = ModelMng.GetModels();

            var TypesList = new List<SelectListItem>();
            var ManufacturersList = new List<SelectListItem>();
            var ModelsList = new List<SelectListItem>();
            var EmployeesList = new List<SelectListItem>();

            foreach(var type in Types)
            {
                TypesList.Add(new SelectListItem { Value = type.Id.ToString(), Text = type.Name });
            }

            foreach(var manufacturer in Manufaturers)
            {
                ManufacturersList.Add(new SelectListItem { Value = manufacturer.Id.ToString(), Text = manufacturer.Name });
            }

            foreach(var model in Models)
            {
                ModelsList.Add(new SelectListItem { Value = model.Id.ToString(), Text = model.Name });
            }

            foreach(var employee in employees)
            {
                //EmployeeNumber is used instead of ID
                EmployeesList.Add(new SelectListItem { Value = employee.EmployeeNumber, Text = $"{employee.FirstName} {employee.LastName}" });
            }

            NewAssetViewModel ViewModel = new NewAssetViewModel();
            ViewModel.AssetType = TypesList;
            ViewModel.Manufacturer = ManufacturersList;
            ViewModel.Model = ModelsList;
            ViewModel.Employees = EmployeesList;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult ProcessData(FormCollection formData)
        {
            try
            {
                AssetManager manager = new AssetManager();
                Asset _asset = new Asset();

                _asset.AssetTypeId = int.Parse(formData["AssetTypes"]);
                _asset.ManufacturerId = int.Parse(formData["Manufacturer"]);
                _asset.ModelId = int.Parse(formData["Model"]);
                _asset.AssignedTo = formData["Employee"];

                manager.AddAsset(_asset);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }         
        }
    }
}