 

using MyWebApp.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.OA.DALFactory
{
    public partial class DALAbstractFactory
    {
      
   
		
	    public static IActionInfoDal CreateActionInfoDal()
        {
           

            return CreateInstance(DalNameSpace + ".ActionInfoDal", DalAssembly) as IActionInfoDal;
        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {
           

            return CreateInstance(DalNameSpace + ".DepartmentDal", DalAssembly) as IDepartmentDal;
        }
		
	    public static IFileInfoDal CreateFileInfoDal()
        {
           

            return CreateInstance(DalNameSpace + ".FileInfoDal", DalAssembly) as IFileInfoDal;
        }
		
	    public static IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {
           

            return CreateInstance(DalNameSpace + ".R_UserInfo_ActionInfoDal", DalAssembly) as IR_UserInfo_ActionInfoDal;
        }
		
	    public static IRoleInfoDal CreateRoleInfoDal()
        {
           

            return CreateInstance(DalNameSpace + ".RoleInfoDal", DalAssembly) as IRoleInfoDal;
        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {
           

            return CreateInstance(DalNameSpace + ".UserInfoDal", DalAssembly) as IUserInfoDal;
        }
		
	    public static IWF_InstanceDal CreateWF_InstanceDal()
        {
           

            return CreateInstance(DalNameSpace + ".WF_InstanceDal", DalAssembly) as IWF_InstanceDal;
        }
		
	    public static IWF_StepInfoDal CreateWF_StepInfoDal()
        {
           

            return CreateInstance(DalNameSpace + ".WF_StepInfoDal", DalAssembly) as IWF_StepInfoDal;
        }
		
	    public static IWF_TempDal CreateWF_TempDal()
        {
           

            return CreateInstance(DalNameSpace + ".WF_TempDal", DalAssembly) as IWF_TempDal;
        }
	}
	
}