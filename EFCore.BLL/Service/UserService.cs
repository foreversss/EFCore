using EFCore.BLL.IService;
using EFCore.DAL.Common.Core;
using EFCore.DAL.Common.Interface;
using EFCore.Entity;
using EFCore.Entity.DB_Entity;
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

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public async Task<Blog_Users> Register(Blog_Users user)
        {
            var service = this.CreateService<Blog_Users>();

            return await service.Add(user);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public async Task<Blog_Users> SignIn(string username, string password)
        {
            var service = this.CreateService<Blog_Users>();

            return await service.FirstOrDefault(x => x.UserName == username && x.PassWord == password && x.IsDelete == false);
        }

        /// <summary>
        /// 修改用户Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserToken(Blog_Users user)
        {
            var service = this.CreateService<Blog_Users>();

            return await service.Update(user);
        }
    }
}
