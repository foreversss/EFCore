using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A.Entity
{
    public class Users
    {
        public int Id { get; set; }
        [Display(Name ="用户名")]
        [Required(ErrorMessage ="请输入用户名"),StringLength(50)]
        public string UserName { get; set; }

        [Display(Name ="密码")]
        [Required(ErrorMessage ="请输入密码")]
        public string PassWord { get; set; }

    }
}
