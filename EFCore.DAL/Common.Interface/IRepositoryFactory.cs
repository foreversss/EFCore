using EFCore.DAL.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.DAL.Common.Interface
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(IconCardContext conCardContext) where T : class;

    }
}
