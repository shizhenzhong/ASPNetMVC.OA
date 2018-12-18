using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using MyWebApp.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{

   

    public class ActionInfoController : Controller
    {
        //
        // GET: /ActionInfo/
        public IActionInfoService actionInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetActionInfo()
        {
            int pageIndex =int.Parse(Request["page"]);
            int pageSize =int.Parse(Request["rows"]);
            int totalCount;
            short delFlag =(short)DeleteEnumType.Normal;
            var actionInfoList = actionInfoService.LoadPagedEntities<string>(pageIndex, pageSize, out totalCount, a => a.DelFlag == delFlag,
                a => a.Sort, true);
            var rows = from a in actionInfoList
                       select new
                       {
                           ID = a.ID,
                           ActionInfoName = a.ActionInfoName,
                           Url = a.Url,
                           ActionTypeEnum= a.ActionTypeEnum,
                           HttpMethod=a.HttpMethod,
                           Sort = a.Sort,
                           a.ControllerName,
                           Remark = a.Remark,
                           SubTime = a.SubTime
                       };
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetMenuIcon()
        {
            HttpPostedFileBase file = Request.Files[0];
            string fileName = Path.GetFileName(file.FileName);
            string fileExt = Path.GetExtension(fileName);
            if (fileExt == ".jpg")
            {
                string newFileName = Guid.NewGuid().ToString() + fileExt;
                file.SaveAs(Server.MapPath("/MenuIcon/" + newFileName));
                return Content("ok:/MenuIcon/" + newFileName);
            }
            else
            {
                return Content("no:");
            }

        }



       public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now ;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            actionInfo.HttpMethod = actionInfo.HttpMethod.ToLower();
            actionInfoService.AddEntity(actionInfo);
            return Content("ok");
        }

    }
}
