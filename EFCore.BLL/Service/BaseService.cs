using EFCore.BLL.IService;
using EFCore.DAL.Common.Core;
using EFCore.DAL.Common.Interface;
using EFCore.Tools.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.BLL.Service
{  
    public class BaseService : IBaseService
    {
        private IRepositoryFactory _repositoryFactory;
        private IconCardContext _dbcontext;
        public BaseService(IRepositoryFactory repositoryFactory, IconCardContext dbcontext)
        {
            this._repositoryFactory = repositoryFactory;
            this._dbcontext = dbcontext;
        }

        public IRepository<T> CreateService<T>() where T : class, new()
        {
            return _repositoryFactory.CreateRepository<T>(_dbcontext);
        }
    }
}
