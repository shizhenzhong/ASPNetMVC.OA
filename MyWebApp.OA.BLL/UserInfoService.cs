
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.BLL
{
    public class UserInfoService: BaseService<UserInfo>,IUserInfoService
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

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }
}