using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPRG102.FinalProject.AssetTracking.ViewModels
{
    public class AssignmentViewModel
    {
        public IEnumerable<SelectListItem> UnassignedAssets { get; set; }

        public IEnumerable<SelectListItem> UnassignedEmployees { get; set; }

        public IEnumerable<SelectListItem> AssetType { get; set; }

        public string employee { get; set; }

        public int type { get; set; }

        public int assetId { get; set; }
    }
}