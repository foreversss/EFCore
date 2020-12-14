using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Modes
{
    public class Movie
    {

        public int ID { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "创建时间")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "类型")]
        public string Genre { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "单价")]
        public decimal Price { get; set; }

    }
}
