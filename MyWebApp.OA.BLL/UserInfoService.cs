
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using MyWebApp.OA.Model.UserInfoSearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyWebApp.OA.BLL
{
    public partial class UserInfoService: BaseService<UserInfo>,IUserInfoService
    {
        public bool DeleteEntities(List<int> list)
        {
           var userInfoList=this.DbSession.UserInfoDal.LoadEntities(u => list.Contains(u.ID));
            if (userInfoList != null)
            {
                foreach (var item in userInfoList)
                {
                    this.DbSession.UserInfoDal.DeleteEntity(item);//将删除的数据添加到EF上下文中并添加删除标记
                }
            }

            return this.DbSession.SaveChanges();//将数据
        }

        public void FindUserPwd(UserInfo userInfo)
        {
            string newPwd = Guid.NewGuid().ToString().Substring(0, 8);
            //将产生的新密码加密后替换用户原来的旧密码
            userInfo.UPwd = newPwd;
            this.DbSession.UserInfoDal.UpdateEntity(userInfo);
            this.DbSession.SaveChanges();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From= new MailAddress("shizhenzhong2013@qq.com", "史振中");
            mailMessage.To.Add(new MailAddress(userInfo.Mail, userInfo.UName));
            mailMessage.Subject = "Hello,大家好！";
            StringBuilder sb = new StringBuilder();
            sb.Append("您的新账户如下:");
            sb.Append("用户名：" + userInfo.UName);
            sb.Append("密码:" + newPwd);
            mailMessage.Body =sb.ToString();
            SmtpClient client=new SmtpClient("smtp.qq.com");//smtp服务器地址
            client.Credentials = new NetworkCredential("shizhenzhong2013@qq.com", "szzszz");//发件人用户名mima 
            client.Send(mailMessage);
        }

        #region 多条件搜索
        public IQueryable<UserInfo> LoadSearchUserInfo(UserInfoSearchParam userInfoSearchParam)
        {
            var temp = this.DbSession.UserInfoDal.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userInfoSearchParam.UName))
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoSearchParam.UName));

            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.URemark))
            {
                temp = temp.Where<UserInfo>(u => u.Remark.Contains(userInfoSearchParam.URemark));
            }

            userInfoSearchParam.TotalCount = temp.Count();

            return temp.Where(u=>u.DelFlag==userInfoSearchParam.DelFlag).OrderBy<UserInfo, string>(u => u.Sort).Skip<UserInfo>((userInfoSearchParam.PageIndex - 1) * userInfoSearchParam
               .PageSize).Take<UserInfo>(userInfoSearchParam.PageSize);
        }

        #region 为用户设置特殊权限
        public bool SetUserAction(int userId, int actionid, bool value)
        {
            var actionInfo = this.DbSession.R_UserInfo_ActionInfoDal.LoadEntities(r => r.UserInfoID == userId && r.ActionInfoID == actionid).FirstOrDefault();
            if (actionInfo == null)
            {
                R_UserInfo_ActionInfo userInfo_ActionInfo = new R_UserInfo_ActionInfo();
                userInfo_ActionInfo.ActionInfoID = actionid;
                userInfo_ActionInfo.UserInfoID = userId;
                userInfo_ActionInfo.IsPass = value;
                this.DbSession.R_UserInfo_ActionInfoDal.AddEntity(userInfo_ActionInfo);
            }
            else
            {
                if (actionInfo.IsPass != value)
                {
                    actionInfo.IsPass = value;
                   
                }
            }

            return this.DbSession.SaveChanges();
        }
        #endregion



        #region 为用户分配角色
        public bool SetUserRole(int userId, List<int> RoleIdList)
        {
            var userInfo = this.DbSession.UserInfoDal.LoadEntities(u => u.ID == userId).FirstOrDefault();
            if (userInfo != null)
            {

                userInfo.RoleInfo.Clear();
                foreach (int roleId in RoleIdList)
                {
                    var roleInfo = this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                    userInfo.RoleInfo.Add(roleInfo);
                }
            }

            return this.DbSession.SaveChanges();

           
        }
        #endregion




        #endregion
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
    }
}