using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BLL.IService;
using EFCore.Entity.DB_Entity;
using EFCore.Entity.Model_Entity;
using EFCore.Tools.Cache;
using EFCore.Tools.Helpers;
using EFCore.Tools.HttpHelperFile;
using EFCore.Tools.Quartz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EFCore.API.Controllers.Users
{

    public class UsersController : BaseController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;

        //缓存服务
        private readonly IMemoryCacheService _memoryCacheService;

        public UsersController(IUserService userService, IConfiguration configuration, IMemoryCacheService memoryCacheService)
        {
            _userService = userService;
            _configuration = configuration;
            _memoryCacheService = memoryCacheService;
        }

       /// <summary>
       /// 登录
       /// </summary>
       /// <param name="userinfo">登录用户名和密码</param>
       /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ExcutedResult>> SignIn(LoginUser userinfo)
        {
            try
            {
                QuartzService.StartJob<QuartzJob>("jobWork1", 1);
                //请求状态
                int statecode = (int)ExcutedResult.status.成功;

              
                var pwd = MD5Helper.MD5Encrypt32(userinfo.PassWord);

                //验证密码是否正确
                var user = await _userService.SignIn(userinfo.UserName, pwd);

                //不正确
                if (user == null)
                {
                    statecode = (int)ExcutedResult.status.账号密码错误;
                    return ExcutedResult.SuccessResult("账号密码错误", statecode);
                }
                
                //从redis里面读取token  如果有值就继续用以前的Token 如果没有就重新生成
                var Token = _memoryCacheService.Get<Blog_Users>(user.UserToken);

                if (Token == null)
                {
                    //登录成功 生成Token
                    JwtTokenUtil jwtTokenUtil = new JwtTokenUtil(_configuration);

                    //生成Token
                    string token = jwtTokenUtil.GetToken(user);
                    user.UserToken = token;

                    //修改用户token
                    var updatetoken = await _userService.UpdateUserToken(user);
                    //修改成功后 并存在redis把用户存在redis缓存当中
                    if (updatetoken > 0)
                    {
                        _memoryCacheService.AddObject(token, user, new TimeSpan(1, 0, 0), false);
                    }                 
                }            

                return ExcutedResult.SuccessResult(user, statecode);
            }
            catch (Exception ex)
            {

                return ExcutedResult.FailedResult(ex.Message, (int)ExcutedResult.status.请求失败);
            }
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ExcutedResult>> Register([FromBody] Blog_Users user)
        {
            try
            {

                //请求状态
                int statecode = (int)ExcutedResult.status.成功;

                user.CreationTime = DateTimeHelper.GettimeStamp();

                //执行添加
                var result = await _userService.Register(user);

                if (result == null)
                {

                    statecode = (int)ExcutedResult.status.添加数据失败;
                    return ExcutedResult.SuccessResult("添加数据失败", statecode);
                }

                return ExcutedResult.SuccessResult(result, statecode);
            }
            catch (Exception ex)
            {

                return ExcutedResult.FailedResult(ex.Message, (int)ExcutedResult.status.请求失败);
            }
        }
    }
}
