
using MyWebApp.OA.Model;
using MyWebApp.OA.Model.UserInfoSearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.IBLL
{
    public interface IUserInfoService:IBaseService<UserInfo>
    {

        /// <summary>
        /// 批量删除用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);

        /// <summary>
        /// 多条件搜索用户信息
        /// </summary>
        /// <param name="userInfoSearchParam"></param>
        /// <returns></returns>
        IQueryable<UserInfo> LoadSearchUserInfo(UserInfoSearchParam userInfoSearchParam);
    }
}