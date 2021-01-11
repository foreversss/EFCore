using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using A.DLL;
using A.Entity;
using A.Entity.DTO_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using StudentMvc.Models;

namespace StudentMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment webHostEnvironment;

        //private readonly IRepository _repository;

        private readonly IStudentRepository _studentRepository;

        public HomeController(ILogger<HomeController> logger,IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.webHostEnvironment = webHostEnvironment;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var studenlist = await _studentRepository.QueryAll();
            return View(studenlist);
        }

        [HttpGet]
        public IActionResult Add()
        {
            throw new Exception("此异常发送在Add'视图中");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel model)
        {

           

            if (ModelState.IsValid)
            {
                var uniqueFileName = string.Empty;
                if (model.PhotoPath != null)
                {
                    var uploadsFoloder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PhotoPath.FileName;

                    string filepath = Path.Combine(uploadsFoloder, uniqueFileName);

                    model.PhotoPath.CopyTo(new FileStream(filepath, FileMode.Create));

                    Student student = new Student()
                    {
                        Grade = model.Grade,
                        Mailbox = model.Mailbox,
                        Name = model.Name,
                        Photo = uniqueFileName
                    };
                    var stu = await _studentRepository.Add(student);

                    if (stu > 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }





            }
            return View();
        }


        public async Task<IActionResult> Delete(int Id)
        {
            var delete = await _studentRepository.DeleteById(Id);

            if (delete)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int Id)
        {
            var student = await _studentRepository.QueryById(Id);

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            if (ModelState.IsValid)
            {
                var i = await _studentRepository.Update(student);

                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}
