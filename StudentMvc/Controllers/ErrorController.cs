using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A.StudentMvc.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> logger;

        /// <summary>
        /// 通过asp.net core 依赖注入服务注入ILogger服务
        /// 将指定的控制器作为泛型参数
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;

        }


        [Route("Error/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {

            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMeagess = "您访问的页面不存在";

                    logger.LogWarning($"发生了一个404错误，路径={statusCodeResult.OriginalPath},路径参数={statusCodeResult.OriginalPathBase}");

                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.PathBase = statusCodeResult.OriginalPathBase;
                    //ViewBag.QueryString = statusCodeResult.OriginalQueryString;

                    break;
            }

            return View();
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPath = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"路径={exceptionHandlerPath.Path}，产生了一个错误{exceptionHandlerPath.Error.Message}");

            //ViewBag.Path = exceptionHandlerPath.Path;
            //ViewBag.Message = exceptionHandlerPath.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPath.Error.StackTrace;
            return View();
        }
    }
}
