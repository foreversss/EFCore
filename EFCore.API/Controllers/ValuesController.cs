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


        //public IActionResult GetALL()
        //{
        //    QuartzService.StartJob<QuartzJob>("jobWork1", 1);
        //    return Ok("11");
        //}
    }
}
