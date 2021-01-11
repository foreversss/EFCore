using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Entity
{
    public class Student
    {
        [Display(Name="编号")]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage ="姓名不能为空"),StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "邮箱")]
        [RegularExpression(@"^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$",ErrorMessage ="邮箱格式不正确")]
        [Required(ErrorMessage = "邮箱不能为空")]      
        public string Mailbox { get; set; }

        [Display(Name = "年级")]
        [Required(ErrorMessage = "年级不能为空")]
        public Grade? Grade { get; set; }

    }
}
