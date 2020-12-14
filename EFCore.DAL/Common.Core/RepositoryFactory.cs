using EFCore.DAL.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.DAL.Common.Core
{

    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> CreateRepository<T>(IconCardContext conCardContext) where T : class
        {
            return new Repository<T>(conCardContext);
        }
    }
}
