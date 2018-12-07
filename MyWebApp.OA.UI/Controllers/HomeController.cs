using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData["username"] = LoginUser.UName;
            }
           
            return View();
        }

    }
}
