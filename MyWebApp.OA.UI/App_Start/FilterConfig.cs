using MyWebApp.OA.UI.Models;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());
        }
    }
}