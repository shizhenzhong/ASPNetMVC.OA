using MyWebApp.OA.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyWebApp.OA.UI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication :Spring.Web.Mvc.SpringMvcApplication// System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string fileLogPath = Server.MapPath("/Log/");//用来保存错误日志的文件夹路径
            //开启一个线程池线程扫描日志队列
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.exceptionQueue.Count > 0)
                    {
                        Exception ex = MyExceptionAttribute.exceptionQueue.Dequeue();
                        if (ex!=null)
                        {
                            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            File.AppendAllText(fileLogPath + fileName, ex.ToString(), Encoding.Default);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);//如果队列中没有数据,让线程休息，避免CPU空转
                    }
                }
            },fileLogPath);
        }
    }
}