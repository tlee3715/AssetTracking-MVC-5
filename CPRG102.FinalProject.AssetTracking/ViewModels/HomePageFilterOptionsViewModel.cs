using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPRG102.FinalProject.AssetTracking.ViewModels
{
    public class HomePageFilterOptionsViewModel
    {
       public IEnumerable<SelectListItem> Employees { get; set; }

       public IEnumerable<SelectListItem> AssetType { get; set; }
    }   
 }