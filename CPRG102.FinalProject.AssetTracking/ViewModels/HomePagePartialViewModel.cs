using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPRG102.FinalProject.AssetTracking.ViewModels
{
    public class HomePagePartialViewModel
    {
        public string AssetDescription { get; set; }
        public string AssetTypeName { get; set; }
        public string TagNumber { get; set; }
        public string SerialNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentLocation { get; set; }
    }
}