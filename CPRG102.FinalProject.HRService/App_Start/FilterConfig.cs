using System.Web;
using System.Web.Mvc;

namespace CPRG102.FinalProject.HRService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
