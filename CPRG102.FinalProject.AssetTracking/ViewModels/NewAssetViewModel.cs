using CPRG102.FinalProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPRG102.FinalProject.AssetTracking.ViewModels
{
    public class NewAssetViewModel
    {
        public IEnumerable<SelectListItem> AssetType { get; set; }

        public IEnumerable<SelectListItem> Manufacturer { get; set; }

        public IEnumerable<SelectListItem> Model { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }

        public Asset asset { get; set; }
    }
}