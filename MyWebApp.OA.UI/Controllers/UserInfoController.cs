using MyWebApp.OA.BLL;
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        IUserInfoService userInfoService = new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserInfo()
        {
            int pageIndex = int.Parse(Request["page"]);//当前页码
            int pageSize = int.Parse(Request["rows"]);//显示的条数
            int totalCount = 0;
            short deleteType = (short)DeleteEnumType.Normal;
            var userInfoList=userInfoService.LoadPagedEntities<int>(pageIndex, pageSize, out totalCount, 
                c =>c.DelFlag== deleteType, c=>c.ID,true);
            var temp = from u in userInfoList
                       select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd,
                           Remark = u.Remark, SubTime = u.SubTime };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
    }
}
