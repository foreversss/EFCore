using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A.Entity
{
    public enum Grade
    {
        [Display(Name = "未分配")]
        Unassigned,

        [Display(Name = "一年级")]
        FirstGrade,

        [Display(Name = "二年级")]
        SecondGrade,

        [Display(Name = "三年级")]
        ThirdGrade
    }
}
