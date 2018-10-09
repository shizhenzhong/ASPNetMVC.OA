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

    /// <summary>
    /// DBsession:数据会话层，负责数据操作类实例的创建，然后业务层调用数据会话层，获取相应的数据操作类实例，
    /// DBSession:其实就是一个工厂类，完成了业务层与数据层的解耦
    /// </summary>
    /// 

   public class DBSession:IDBSession
    {

        DbContext Db = new OAEntities();
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

       
        /// <summary>
        /// 保存数据，将要操作的数据先添加到EF上下文中，然后再统一保存到数据库中，
        /// 就完成了链接一次数据库完成了多次操作，提高了数据操作的性能
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
