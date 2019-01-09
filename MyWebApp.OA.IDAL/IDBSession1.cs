 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace MyWebApp.OA.IDAL
{
    public partial interface IDBSession
    {
	
	 IActionInfoDal ActionInfoDal { get; set; }
	
	 IBillDal BillDal { get; set; }
	
	 IBillTypeDal BillTypeDal { get; set; }
	
	 ICurrentUnitDal CurrentUnitDal { get; set; }
	
	 IDepartmentDal DepartmentDal { get; set; }
	
	 IFileInfoDal FileInfoDal { get; set; }
	
	 IPayTypeDal PayTypeDal { get; set; }
	
	 IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal { get; set; }
	
	 IRoleInfoDal RoleInfoDal { get; set; }
	
	 IUserInfoDal UserInfoDal { get; set; }
	
	 IWF_InstanceDal WF_InstanceDal { get; set; }
	
	 IWF_StepInfoDal WF_StepInfoDal { get; set; }
	
	 IWF_TempDal WF_TempDal { get; set; }
	}
}