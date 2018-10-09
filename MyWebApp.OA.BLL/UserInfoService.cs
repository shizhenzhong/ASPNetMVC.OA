
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.BLL
{
    public class UserInfoService:BaseService<UserInfo>
    {
        public override void SetCurrentDal()
        {
            CurentDal = this.DbSession.UserInfoDal;
        }
    }
}