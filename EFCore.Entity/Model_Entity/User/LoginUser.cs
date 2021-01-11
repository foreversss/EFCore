using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entity.Model_Entity
{
    /// <summary>
    /// 用户登录参数类
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

    }
}
