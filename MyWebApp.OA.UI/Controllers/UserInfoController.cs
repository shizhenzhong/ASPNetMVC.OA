
using MyWebApp.OA.BLL;
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using MyWebApp.OA.Model.Enum;
using MyWebApp.OA.Model.UserInfoSearch;
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

        public  IUserInfoService userInfoService { get; set; } //new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }



        #region 获取用户信息
        public ActionResult GetUserInfo()
        {
            string name = Request["name"];
            string remark = Request["remark"];
            int pageIndex = int.Parse(Request["page"]);//当前页码
            int pageSize = int.Parse(Request["rows"]);//显示的条数
            int totalCount = 0;
            short deleteType = (short)DeleteEnumType.Normal;
            UserInfoSearchParam userInfoSearchParam = new UserInfoSearchParam
            {
                UName = name,
                URemark = remark,
                DelFlag = deleteType,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount

            };
           
            var userInfoList = userInfoService.LoadSearchUserInfo(userInfoSearchParam);
            //var userInfoList=userInfoService.LoadPagedEntities<string>(pageIndex, pageSize, out totalCount, 
            //    c =>c.DelFlag== deleteType, c=>c.Sort,true);
            var temp = from u in userInfoList
                       select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd,
                           Remark = u.Remark, SubTime = u.SubTime,DelFlag=u.DelFlag,Sort=u.Sort  };
            return Json(new { rows = temp, total = userInfoSearchParam.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除用户信息

        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(int.Parse(id));
            }

            if (userInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        #endregion

        #region 添加用户信息
        [HttpPost]
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.Sort = "0";
            userInfo.DelFlag = 0;
            userInfoService.AddEntity(userInfo);

            return Content("ok");
        }
        #endregion

        #region 修改用户信息
        public ActionResult  EditUserInfo(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            if (userInfoService.UpdateEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }


        #endregion

        #region 获取要修改的用户信息
        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
            var userinfo = userInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            ViewData.Model = userinfo;
            return View();
        }
        #endregion
    }
}
