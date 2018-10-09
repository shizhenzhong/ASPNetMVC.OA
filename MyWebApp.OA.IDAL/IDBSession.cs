
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.IDAL
{
    public interface IDBSession
    {
        IUserInfoDal UserInfoDal { get; set; }
        bool SaveChanges();
    }
}