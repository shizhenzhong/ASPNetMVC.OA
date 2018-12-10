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
            return View();
        }

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
            string pwd = Request["LoginPwd"];
            var userInfo=userInfoService.LoadEntities(u => u.UName == username && u.UPwd == pwd).FirstOrDefault();
            if (userInfo == null)
            {
                return Content("no:用户名或密码错误");
            }
            else
            {
                //Session["userInfo"] = userInfo;
                //string seesionID = Guid.NewGuid().ToString();//自己创建的SessionID作为Memcache的Key
                string seesionID =Request.Cookies["ASP.NET_SessionId"].Value;//获取sessionID作为Memcache的Key
                Common.MemcacheHelper.Set(seesionID, Common.SerializerHelper.SerializerToString(userInfo));
               // Response.Cookies["sessionID"].Value = seesionID;//以cookie的形式返回到浏览器端
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
            var userInfo = userInfoService.LoadEntities(u => u.UName == txtName && u.Mail == txtMail).FirstOrDefault();
            return View();
        }
        #endregion
    }
}
