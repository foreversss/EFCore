using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entity.DB_Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Blog_Users
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mailbox { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? HeadPortrait { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreationTime { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string? Describe { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string? SchoolName { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public int? GraduationTime { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string? CompanyName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 用户Token
        /// </summary>
        public string? UserToken { get; set; }



    }
}
