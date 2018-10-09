using MyWebApp.OA.DAL;
using MyWebApp.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.OA.DALFactory
{

    /// <summary>
    /// DBsession:数据会话层，负责数据操作类实例的创建，然后业务层调用数据会话层，获取相应的数据操作类实例，
    /// DBSession:其实就是一个工厂类，完成了业务层与数据层的解耦
    /// </summary>
    /// 

   public class DBSession:IDBSession
    {
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
    }
}
