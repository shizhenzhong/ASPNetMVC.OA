
using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyWebApp.OA.BLL
{
    public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        public bool DeleteEntities(List<int> list)
        {
            var actionInfos = this.DbSession.ActionInfoDal.LoadEntities(c => list.Contains(c.ID));

            foreach (var item in actionInfos)
            {
                this.DbSession.ActionInfoDal.DeleteEntity(item);
            }

            return this.DbSession.SaveChanges();
        }
    }
}