using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using Spring.Context;
using Spring.Context.Support;
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
            if (Request.Cookies["sessionID"] != null)
            {
                string sessionID = Request.Cookies["sessionID"].Value;
                object obj=Common.MemcacheHelper.Get(sessionID);
                if (obj != null)
                {
                    LoginUser=Common.SerializerHelper.DeSerializerToObject<UserInfo>(obj.ToString());
                    isExt = true;

                    //完成权限过滤
                    if (LoginUser.UName == "itcast")
                    {
                        return;
                    }
                    string requstUrl = Request.Url.AbsolutePath.ToLower();//获取url
                    string requestMethod = Request.HttpMethod.ToLower();//获取请求方式
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("userInfoService");
                    IActionInfoService actionInfoService=(IActionInfoService)ctx.GetObject("actionInfoService");
                    var currentAction = actionInfoService.LoadEntities(a => a.Url.ToLower() == requstUrl &&
                      a.HttpMethod.ToLower() == requestMethod).FirstOrDefault();
                    if (currentAction == null)
                    {
                        Response.Redirect("/Error.html");
                        return;
                    }
                    //通过1号线进行校验 
                    var userInfo = userInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
                    var actions = userInfo.R_UserInfo_ActionInfo.Where(r => r.ActionInfoID == currentAction.ID).FirstOrDefault();
                    if (actions != null)
                    {
                        if (actions.IsPass == true)
                        {
                            return;
                        }
                        else
                        {
                            Response.Redirect("/actioninfo.html");
                            return;
                        }
                    }

                    //2号线
                    var currentUserRoles = userInfo.RoleInfo;
                    var currentUserActions = from a in currentUserRoles
                                            select a.ActionInfo;
                    var count = (from a in currentUserActions
                                 from b in a
                                 where b.ID == currentAction.ID
                                 select b).Count();
                    if (count < 1)
                    {
                        Response.Redirect("/actioninfo.html");
                        return;
                    }
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