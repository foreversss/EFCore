using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BLL.IService;
using EFCore.Entity;
using EFCore.Tools.Attributes;
using EFCore.Tools.Cache;
using EFCore.Tools.Helpers;
using EFCore.Tools.Quartz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quartz;

namespace EFCore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUserService _userService { get; set; }

        private IMemoryCacheService _cacheService { get; set; }

        public ValuesController(IUserService userService, IMemoryCacheService cacheService)
        {
            this._userService = userService;
            this._cacheService = cacheService;
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetAllUser()
        {

            QuartzService.StartJob<QuartzJob>("jobWork1",10);

           //获取缓存
           var list = _cacheService.Get<List<tb_users>>("UserList");

          
            //判断缓存是否过期
            if (list == null)
            {
                list = this._userService.users();
                            
                _cacheService.AddObject("UserList", list, new TimeSpan(0, 2, 36), true);
            }

            return new JsonResult(list);          
        }
    }
}
