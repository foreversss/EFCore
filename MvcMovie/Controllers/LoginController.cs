using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EFCore.BLL.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class LoginController : Controller
    {

        /// <summary>
        /// 用户服务
        /// </summary>
        private IUserService _userService { get; set; }

        /// <summary>
        /// 通过构造函数注入
        /// </summary>
        /// <param name="userService"></param>
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
            
    }
}
