using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welocome(string name, int numTimes = 1)
        {
            //控制器传数据给视图的 四个方法 ViewData ViewBag View(model) TempData 
            //ViewData["Name"] = "Hello " + name;
            //ViewData["NumTimes"] = numTimes;

            //ViewBag.Name = "Hello " + name;
            //ViewBag.NumTimes = numTimes;


            var obj = new
            {
                Name = name,
                NumTimes = numTimes
            };




            // return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
            return View(obj);
        }
    }
}
