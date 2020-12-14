using EFCore.DAL.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.BLL.IService
{
    public interface IBaseService
    {
        IRepository<T> CreateService<T>() where T : class, new();
    }
}
