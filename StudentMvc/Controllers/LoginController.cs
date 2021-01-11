using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using A.DLL;
using A.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace A.StudentMvc.Controllers
{
    public class LoginController : Controller
    {
        private IUsersRepository _usersRepository;

        public LoginController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [HttpGet]
        public IActionResult Logining()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logining(Users users)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersRepository.Query(x => x.UserName == users.UserName && x.PassWord == users.PassWord);

                if (user == null)
                {
                    ViewBag.Message = "账号密码错误";
                    return View();
                }

                //用Claim来构造一个ClaimsIdentity，然后调用 SignInAsync 方法。
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim("usName", "12321"));

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }


            return View();
        }
    }
}
