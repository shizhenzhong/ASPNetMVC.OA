
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using MyWebApp.OA.Model.UserInfoSearch;
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
        #endregion
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }
}