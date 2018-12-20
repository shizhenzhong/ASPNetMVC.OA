 
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MyWebApp.OA.IDAL
{
   
	
	public partial interface IActionInfoDal :IBaseDal<ActionInfo>
    {
      
    }
	
	public partial interface IDepartmentDal :IBaseDal<Department>
    {
      
    }
	
	public partial interface IFileInfoDal :IBaseDal<FileInfo>
    {
      
    }
	
	public partial interface IR_UserInfo_ActionInfoDal :IBaseDal<R_UserInfo_ActionInfo>
    {
      
    }
	
	public partial interface IRoleInfoDal :IBaseDal<RoleInfo>
    {
      
    }
	
	public partial interface IUserInfoDal :IBaseDal<UserInfo>
    {
      
    }
	
	public partial interface IWF_InstanceDal :IBaseDal<WF_Instance>
    {
      
    }
	
	public partial interface IWF_StepInfoDal :IBaseDal<WF_StepInfo>
    {
      
    }
	
	public partial interface IWF_TempDal :IBaseDal<WF_Temp>
    {
      
    }
	
}