using BaseLib.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace A.BaseLib.Repository
{
    /// <summary>
    /// 从内存拿数据
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MemoryRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public Task<int> Add(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Query(Expression<Func<TEntity, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> QueryById(object objId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity entity, string strWhere)
        {
            throw new NotImplementedException();
        }
    }
}
