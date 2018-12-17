using MyWebApp.OA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        IBLL.IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            CheckCookieInfo();
            return View();
        }

        #region 校验Cookie信息
        public void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string cookieUserName = Request.Cookies["cp1"].Value;
                string cookieUserPwd = Request.Cookies["cp2"].Value;
                var userInfo = userInfoService.LoadEntities(u => u.UName == cookieUserName).FirstOrDefault();
                if (userInfo!= null)
               
                {
                    //注意：.，要将用户密码加密以后写到用户表中，如果在添加是已经进行两次MD5运算，那么这里直接比较.
                    string md5Pwd = Common.WebCommon.Md5String(userInfo.UPwd);
                    if (md5Pwd == cookieUserPwd)
                    {
                        string sessionId = Guid.NewGuid().ToString();//自己创建的SessionId,作为Memcache的key.
                        Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo));//将用户的信息存储到Memcache中。
                        Response.Cookies["sessionId"].Value = sessionId;//然后将自创的sessionId以Cookie的形式返回到浏览器，存储到浏览器端的内存中。
                        Response.Redirect("/home/index");
                    }
                }
                //删除Cookie.
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion


        #region 展示验证码
        public ActionResult CreateValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion

        #region 用户登录
        public ActionResult CheckLogin()
        {
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if ( string.IsNullOrEmpty(Request["vCode"]))
            {
               
                return Content("no:请输入验证码");
            }
           
            string requestCode = Request["vCode"];
            if(!requestCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                Session["validateCode"] = null;
                return Content("no:验证码错误");
            }

            string username = Request["LoginCode"];
            string pwd = Common.WebCommon.Md5String(Request["LoginPwd"]);
            var userInfo=userInfoService.LoadEntities(u => u.UName == username && u.UPwd ==pwd).FirstOrDefault();
            if (userInfo == null)
            {
                return Content("no:用户名或密码错误");
            }
            else
            {
                //Session["userInfo"] = userInfo;
                string seesionID = Guid.NewGuid().ToString();//自己创建的SessionID作为Memcache的Key
               
                Common.MemcacheHelper.Set(seesionID, Common.SerializerHelper.SerializerToString(userInfo));
                Response.Cookies["sessionID"].Value = seesionID;//以cookie的形式返回到浏览器端

                if (!string.IsNullOrEmpty(Request["checkMe"]))
                {
                    HttpCookie cookie1 = new HttpCookie("cp1", username);
                    HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.Md5String(pwd));
                    cookie1.Expires = DateTime.Now.AddDays(3);
                    cookie2.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }

                return Content("ok");
            }
        }

        #endregion


        #region 找回密码

        public ActionResult FindPwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindPwd(FormCollection formCollection)
        {
            string txtName = Request["txtName"];
            string txtMail = Request["txtNail"];
            var userInfo = userInfoService.LoadEntities(u => u.UName == txtName).FirstOrDefault();
            if (userInfo != null)
            {
                if (txtMail == userInfo.Mail)
                {

                }
                else
                {
                    return Content("邮箱错误");
                }
            }
            else
            {
                return Content("没有此人");
            }
            return View();
        }
        #endregion




        public ActionResult LoginOut()
        {
            if (Request.Cookies["sessionID"] != null)
            {
                string key = Request.Cookies["sessionID"].Value;
                Common.MemcacheHelper.Delete(key);
                if (Request.Cookies["cp1"]!=null && Request.Cookies["cp2"] != null)
                {
                    Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                }
                


            }


            return Redirect("/login/index");
        }
    }
}
