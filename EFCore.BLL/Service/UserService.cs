using EFCore.BLL.IService;
using EFCore.DAL.Common.Core;
using EFCore.DAL.Common.Interface;
using EFCore.Entity;
using EFCore.Tools.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.BLL.Service
{
    public class UserService : BaseService, IUserService
    {

        public UserService(IRepositoryFactory repositoryFactory, IconCardContext dbcontext) : base(repositoryFactory, dbcontext)
        {

        }

        public List<tb_users> users()
        {
            var service = this.CreateService<tb_users>();
            return  service.GetAll().ToList();
        }
    }
}
