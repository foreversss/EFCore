using BaseLib.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private EFCoreDBContext _dbContext;

        private DbSet<TEntity> _dbset;

        public BaseRepository(EFCoreDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<TEntity>();
        }

        public async Task<int> Add(TEntity model)
        {

            await _dbset.AddAsync(model);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(TEntity model)
        {
            _dbset.Remove(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteById(object id)
        {
            var model = await _dbset.FindAsync(id);
            _dbset.Remove(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            List<TEntity> entities = await Task.Run(async () =>
            {
                var listentity = new List<TEntity>();

                foreach (var item in ids)
                {
                    var model = await _dbset.FindAsync(item);
                    if (model != null)
                    {
                        listentity.Add(model);
                    }
                }

                return listentity;
            });
            _dbset.RemoveRange(entities);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public List<TEntity> GetAll()
        {
            return _dbset.ToList();
        }

        public async Task<TEntity> Query(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(whereLambda);
        }

        public async Task<List<TEntity>> QueryAll()
        {
            return await Task.Run(() => _dbset.ToListAsync());
        }

        public async Task<TEntity> QueryById(object objId)
        {
            return await _dbset.FindAsync(objId);
        }

        public Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(TEntity model)
        {
            var entity = _dbset.Attach(model);
            entity.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public Task<bool> Update(TEntity entity, string strWhere)
        {
            throw new NotImplementedException();
        }
    }
}
