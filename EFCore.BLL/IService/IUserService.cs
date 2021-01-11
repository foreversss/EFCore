using EFCore.Entity;
using EFCore.Entity.DB_Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.BLL.IService
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名称</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<Blog_Users> SignIn(string username,string password);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        Task<Blog_Users> Register(Blog_Users user);

        Task<int> UpdateUserToken(Blog_Users user);

    }
}
