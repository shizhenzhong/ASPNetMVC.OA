
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Models
{
    public class MyExceptionAttribute:HandleErrorAttribute
    {
        public static Queue<Exception> exceptionQueue = new Queue<Exception>();
        
        public override void OnException(ExceptionContext filterContext)
        {
            exceptionQueue.Enqueue(filterContext.Exception);
            filterContext.HttpContext.Response.Redirect("/Error.html");
            base.OnException(filterContext);
        }
    }
}