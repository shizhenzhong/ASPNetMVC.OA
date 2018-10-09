using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.OA.IDAL
{
   public  interface IBaseDal<T> where T: class ,new()
    {
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPagedEntities<s>(int pageIndex, int pageSize, out int totalCount,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, s>> orderbyLambda,
            bool isAsc
            );

        bool DeleteEntity(T entity);
        bool UpdateEntity(T entity);
        T AddEntity(T entity);
    }
}
