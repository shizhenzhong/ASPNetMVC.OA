
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.BLL
{
    public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        public  bool DeleteEntities(List<int> list)
        {
            var roleInfos = this.DbSession.RoleInfoDal.LoadEntities(c => list.Contains(c.ID));

            foreach (var item in roleInfos)
            {
                this.DbSession.RoleInfoDal.DeleteEntity(item);
            }

            return this.DbSession.SaveChanges();
        }
    }
}