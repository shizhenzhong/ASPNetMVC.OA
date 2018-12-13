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

    /// <summary>
    /// 抽象工厂：都是解决对象的创建问题，（通过反射的方式创建对象的实例）
    /// </summary>
    public partial class DALAbstractFactory
    {
        private  static readonly string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];//获取命名空间
        private static readonly string DalAssembly = ConfigurationManager.AppSettings["DalAssembly"];//获取程序集


        //public static IUserInfoDal CreateUserInfoDal()
        //{
           

        //    return CreateInstance(DalNameSpace + ".UserInfoDal", DalAssembly) as IUserInfoDal;
        //}

        private static object CreateInstance(string fullClassName,string assemblyPath)
        {
           var assembly= Assembly.Load(assemblyPath);
           return  assembly.CreateInstance(fullClassName);
        }


    }
}
