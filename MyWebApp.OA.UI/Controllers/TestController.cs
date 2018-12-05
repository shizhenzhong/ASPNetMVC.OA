using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {

            int a = 2;
            int b = 0;
            int c = a / b;

            return Content(c.ToString());
        }

    }
}
