using MyWebApp.OA.BLL;
using MyWebApp.OA.Model;
using MyWebApp.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class RoleInfoController : Controller
    {
        //
        // GET: /RoleInfo/

        RoleInfoService roleInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRoleInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            int totlaCount;
            short delFlag = (short)DeleteEnumType.Normal;
            var roleInfoList = roleInfoService.LoadPagedEntities<string>(pageIndex, pageSize,
                out totlaCount, r => r.DelFlag == delFlag, r => r.Sort, true);
            var rows = from r in roleInfoList
                       select new { ID = r.ID, RoleName = r.RoleName, Sort = r.Sort, Remark = r.Remark, SubTime = r.SubTime };
            return Json(new { rows = rows, total = totlaCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = 0;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.Sort = "0";
            roleInfoService.AddEntity(roleInfo);
            return Content("ok");
        }

        public ActionResult DeleteRoleInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string item in strIds)
            {
                list.Add(int.Parse(item));
            }
            roleInfoService.DeleteEntities(list);
            return Content("ok");
        }


        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
            ViewData.Model = roleInfoService.LoadEntities(r => r.ID == id).FirstOrDefault();
            return View();
        }

        public ActionResult EditInfo(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            if (roleInfoService.UpdateEntity(roleInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

    }
}
