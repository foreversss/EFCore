using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> QueryAll();

        List<TEntity> GetAll();

        Task<TEntity> Query(Expression<Func<TEntity, bool>> whereLambda);

        Task<TEntity> QueryById(object objId);
        Task<List<TEntity>> QueryByIDs(object[] lstIds);

        Task<int> Add(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);

        Task<bool> Delete(TEntity model);

        Task<bool> DeleteByIds(object[] ids);

        Task<bool> Update(TEntity model);
        Task<bool> Update(TEntity entity, string strWhere);
    }
}
