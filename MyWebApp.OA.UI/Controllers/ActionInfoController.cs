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
        public IRoleInfoService roleInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetActionInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            int totalCount;
            short delFlag = (short)DeleteEnumType.Normal;
            var actionInfoList = actionInfoService.LoadPagedEntities<string>(pageIndex, pageSize, out totalCount, a => a.DelFlag == delFlag,
                a => a.Sort, true);
            var rows = from a in actionInfoList
                       select new
                       {
                           ID = a.ID,
                           ActionInfoName = a.ActionInfoName,
                           Url = a.Url,
                           ActionTypeEnum = a.ActionTypeEnum,
                           HttpMethod = a.HttpMethod,
                           Sort = a.Sort,
                           a.ControllerName,
                           Remark = a.Remark,
                           SubTime = a.SubTime,
                           ModifiedOn = a.ModifiedOn
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
            actionInfo.ModifiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            actionInfo.HttpMethod = actionInfo.HttpMethod.ToLower();
            actionInfoService.AddEntity(actionInfo);
            return Content("ok");
        }



        public ActionResult DeleteActionInfo()
        {
            string strs = Request["strId"];
            string[] strId = strs.Split(',');
            List<int> list = new List<int>();
            foreach (var item in strId)
            {
                list.Add(int.Parse(item));
            }
            if (actionInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }


        }


        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.Action = actionInfoService.LoadEntities(a => a.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult EditInfo(ActionInfo actionInfo)
        {

            if (actionInfo.ActionTypeEnum == 0)
            {
                actionInfo.MenuIcon = "";
            }
            actionInfo.ModifiedOn = DateTime.Now;
            if (actionInfoService.UpdateEntity(actionInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }


        public ActionResult SetActionRole()
        {
            int id = int.Parse(Request["id"]);
            var actionInfo = actionInfoService.LoadEntities(a => a.ID == id).FirstOrDefault();
            ViewBag.ActionInfo = actionInfo;
            short delFlag = (short)DeleteEnumType.Normal;
            ViewBag.AllRoles = roleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();
            ViewBag.AllExtRoles = (from r in actionInfo.RoleInfo
                                   select r.ID).ToList();
            return View();

        }

        [HttpPost]
        public ActionResult SetActionRole(FormCollection formCollection)
        {
            int actionId = int.Parse(Request["actionId"]);
            string[] keys = Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach (var key in keys)
            {
                if (key.StartsWith("cba_"))
                {
                    string id = key.Replace("cba_", "");
                    list.Add(int.Parse(id));
                }
            }

            actionInfoService.setActionRoleInfo(actionId, list);
            return Content("ok");
        }
    }
}
