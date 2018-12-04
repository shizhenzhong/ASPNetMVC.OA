
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.IBLL
{
    public interface IUserInfoService:IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);
    }
}