using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class BaseController:Controller
    {

        public UserInfo LoginUser { get; set; }
        //执行控制器方法之前先执行该方法
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isExt = false;
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                string sessionID = Request.Cookies["ASP.NET_SessionId"].Value;
                object obj=Common.MemcacheHelper.Get(sessionID);
                if (obj != null)
                {
                    LoginUser=Common.SerializerHelper.DeSerializerToObject<UserInfo>(obj.ToString());
                    isExt = true;
                }
            }

            if (!isExt)//用户没有登录
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}