using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class HomeController :BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (LoginUser!= null){
                ViewData["userName"] = LoginUser.UName.ToString();
            }
            
           
            return View();
        }


        public ActionResult LoginOut()
        {
            if (Request.Cookies["sessionID"] != null)
            {
                string key = Request.Cookies["sessionID"].Value;
                Common.MemcacheHelper.Delete(key);
                Request.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Request.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                

            }

          
            return Redirect("/Login/Index");
        }



    }
}
