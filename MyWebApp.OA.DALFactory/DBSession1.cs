 
using MyWebApp.OA.DAL;
using MyWebApp.OA.IDAL;
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.OA.DALFactory
{
   
   public partial class DBSession:IDBSession
   {
	
	  private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get {

                if (_ActionInfoDal == null)
                {
                    _ActionInfoDal = DALAbstractFactory.CreateActionInfoDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _ActionInfoDal;
            }
            set {
                _ActionInfoDal = value;
            }
        }
	
	  private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get {

                if (_DepartmentDal == null)
                {
                    _DepartmentDal = DALAbstractFactory.CreateDepartmentDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _DepartmentDal;
            }
            set {
                _DepartmentDal = value;
            }
        }
	
	  private IFileInfoDal _FileInfoDal;
        public IFileInfoDal FileInfoDal
        {
            get {

                if (_FileInfoDal == null)
                {
                    _FileInfoDal = DALAbstractFactory.CreateFileInfoDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _FileInfoDal;
            }
            set {
                _FileInfoDal = value;
            }
        }
	
	  private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get {

                if (_R_UserInfo_ActionInfoDal == null)
                {
                    _R_UserInfo_ActionInfoDal = DALAbstractFactory.CreateR_UserInfo_ActionInfoDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set {
                _R_UserInfo_ActionInfoDal = value;
            }
        }
	
	  private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get {

                if (_RoleInfoDal == null)
                {
                    _RoleInfoDal = DALAbstractFactory.CreateRoleInfoDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _RoleInfoDal;
            }
            set {
                _RoleInfoDal = value;
            }
        }
	
	  private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get {

                if (_UserInfoDal == null)
                {
                    _UserInfoDal = DALAbstractFactory.CreateUserInfoDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _UserInfoDal;
            }
            set {
                _UserInfoDal = value;
            }
        }
	
	  private IWF_InstanceDal _WF_InstanceDal;
        public IWF_InstanceDal WF_InstanceDal
        {
            get {

                if (_WF_InstanceDal == null)
                {
                    _WF_InstanceDal = DALAbstractFactory.CreateWF_InstanceDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _WF_InstanceDal;
            }
            set {
                _WF_InstanceDal = value;
            }
        }
	
	  private IWF_StepInfoDal _WF_StepInfoDal;
        public IWF_StepInfoDal WF_StepInfoDal
        {
            get {

                if (_WF_StepInfoDal == null)
                {
                    _WF_StepInfoDal = DALAbstractFactory.CreateWF_StepInfoDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _WF_StepInfoDal;
            }
            set {
                _WF_StepInfoDal = value;
            }
        }
	
	  private IWF_TempDal _WF_TempDal;
        public IWF_TempDal WF_TempDal
        {
            get {

                if (_WF_TempDal == null)
                {
                    _WF_TempDal = DALAbstractFactory.CreateWF_TempDal();//通过抽象工厂将数据会话层与数据层解耦
                }
                return _WF_TempDal;
            }
            set {
                _WF_TempDal = value;
            }
        }
	}
}