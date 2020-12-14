using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entity
{
    
    public class tb_users
    {
       
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
       
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreationTime { get; set; }
    }
}
