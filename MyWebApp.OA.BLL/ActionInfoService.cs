
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

        public bool setActionRoleInfo(int actionId, List<int> list)
        {
            var actionInfo = this.DbSession.ActionInfoDal.LoadEntities(a => a.ID == actionId).FirstOrDefault();
            if (actionInfo != null)
            {
                actionInfo.RoleInfo.Clear();
                foreach (var roleId in list)
                {
                    var roleInfo = this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                    actionInfo.RoleInfo.Add(roleInfo);
                }
            }
            return this.DbSession.SaveChanges();
        }
    }
}