using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model.ActionEquelCompare;
using MyWebApp.OA.Model.Enum;
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
        public IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            if (LoginUser!= null){
                ViewData["userName"] = LoginUser.UName.ToString();
            }
            
           
            return View();
        }



        #region 找出用户对应的菜单权限
        public ActionResult GetMenuItems()
        {
            //1查询用户已经有的角色
            var userInfo = userInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
            var userRoles = userInfo.RoleInfo;
            //2找出对应的权限
            short menuType =(short)ActionTypeEnum.MenuActionType;
            var userMenuItems = (from r in userRoles
                                 from a in r.ActionInfo
                                 where a.ActionTypeEnum == menuType
                                 select a).ToList() ;


            //3 找出用户特有的权限
            var userActions = userInfo.R_UserInfo_ActionInfo;
            //4找出userActions中允许的权限
            var isPassUserActions = (from a in userActions
                                    where a.IsPass == true && a.ActionInfo.ActionTypeEnum == menuType
                                   select a);
            var isPassActions = (from a in isPassUserActions
                                 select a.ActionInfo).ToList();
            userMenuItems.AddRange(isPassActions);

            //找出禁止权限
            var isNotPassUserActions = (from a in userActions
                                       where a.IsPass ==false
                                       select a.ActionInfoID).ToList();
            userMenuItems = userMenuItems.Where(a => !isNotPassUserActions.Contains(a.ID)).ToList();
            userMenuItems=userMenuItems.Distinct(new ActionEquelCompare()).ToList();
            JsonResult jsonResult = null;
            try
            {
                var result = from u in userMenuItems
                             select new
                             {
                                 icon = u.MenuIcon,
                                 title = u.ActionInfoName,
                                 url = u.Url
                             };


                jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {

            }
            return jsonResult;
        }
        #endregion

    }
}
