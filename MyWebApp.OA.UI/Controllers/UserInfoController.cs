
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

        public IUserInfoService userInfoService { get; set; } //new UserInfoService();
        public IRoleInfoService roleInfoService { get; set; }
        public IActionInfoService actionInfoService { get; set; }
        public IR_UserInfo_ActionInfoService r_UserInfo_ActionInfoService { get; set; }
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
                           Remark = u.Remark, SubTime = u.SubTime, DelFlag = u.DelFlag, Sort = u.Sort };
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
            userInfo.UPwd = Common.WebCommon.Md5String(userInfo.UPwd);
            userInfoService.AddEntity(userInfo);

            return Content("ok");
        }
        #endregion

        #region 修改用户信息
        public ActionResult EditUserInfo(UserInfo userInfo)
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


       
        public ActionResult SetUserRoleInfo()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = userInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            ViewBag.UserInfo = userInfo;
            short Delflag = (short)DeleteEnumType.Normal;
            ViewBag.AllRoles = roleInfoService.LoadEntities(r=>r.DelFlag==Delflag).ToList();
            ViewBag.ExtRoles = (from r in userInfo.RoleInfo
                                select r.ID).ToList();//获取当前用户已经有的角色的编号

            return View();
        }

        [HttpPost]
        public ActionResult SetUserRoleInfo(FormCollection collection)
        {
            int userId = int.Parse(Request["userId"]);
            string[] AllKeys = Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach (string key in AllKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string roleId = key.Replace("cba_", "");
                    list.Add(int.Parse(roleId));
                }
            }

            userInfoService.SetUserRole(userId, list);
            return Content("ok");
            
        }


        public ActionResult SetUserActionInfo()
        {
            int userId = int.Parse(Request["id"]);
            var userInfo=userInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();
            ViewData.Model = userInfo;
            ViewBag.UserInfo = userInfo;
            short delFlag = (short)DeleteEnumType.Normal;
            ViewBag.AllActions = actionInfoService.LoadEntities(a => a.DelFlag == delFlag).ToList();//找出所有的权限
            ViewBag.AllExtActions = userInfo.R_UserInfo_ActionInfo.ToList();//当前用户所有的权限
            return View();
        }
        [HttpPost]
        public ActionResult SetActionForUser()
        {
            int userid =int.Parse(Request["userid"]);
            int actionid = int.Parse(Request["actionid"]);
            string value = Request["value"];
            bool isPass = value == "true" ? true : false;
           if(userInfoService.SetUserAction(userid, actionid, isPass))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult ClearActionUser()
        {
            int userid = int.Parse(Request["userid"]);
            int actionid = int.Parse(Request["actionid"]);
            var actionInfo = r_UserInfo_ActionInfoService.LoadEntities(r => r.UserInfoID == userid && r.ActionInfoID == actionid).FirstOrDefault();
            if (actionInfo != null)
            {
                r_UserInfo_ActionInfoService.DeleteEntity(actionInfo);
            }
            return Content("ok");
        }
    }
}
