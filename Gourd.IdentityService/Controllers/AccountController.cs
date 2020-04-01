using Gourd.IdentityService.Common;
using Gourd.IdentityService.Model;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.IdentityService.Controllers
{

    public class AccountController : Controller
    {
       
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ApplicationDbContext _appcontext;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
         SignInManager<IdentityUser> signInManager,
         IIdentityServerInteractionService interaction,
         ApplicationDbContext appcontext,
        TestUserStore users = null)
        {

            _signInManager = signInManager;
            _interaction = interaction;
            _appcontext = appcontext;
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)){
                var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
                if (context == null)
                {
                    HttpContext.Session.SetString("RedirectUri", returnUrl);
                }
                else
                {
                    HttpContext.Session.SetString("RedirectUri", context.RedirectUri.Replace("signin-oidc", ""));
                }
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Info()
        {
            var userinfo = from c in User.Claims select new { c.Type, c.Value };
            return View(userinfo);
        }


        [HttpPost]
        public async Task<JsonResult> Login(string user_name, string password, string code)
        {
            JsonResponse json = new JsonResponse();
            string pic_code = HttpContext.Session.GetString("pic_code");

            var RedirectUri = HttpContext.Session.GetString("RedirectUri");
            if (!string.IsNullOrEmpty(RedirectUri))
            {
                json.data = RedirectUri;
            }
            else
            {
                json.data = "/";
            }

            using (var md5 = MD5.Create())
            {
                var res = md5.ComputeHash(Encoding.UTF8.GetBytes(code.ToLower()));
                code = BitConverter.ToString(res);
            }

            if (string.IsNullOrEmpty(pic_code))
            {
                json.status = -1;
                json.msg = "请获取验证码";
                return Json(json);
            }
            if (pic_code != code)
            {
                json.status = -1;
                json.msg = "验证码不正确";
                return Json(json);
            }

            var userInfo = await _appcontext.Users.FirstAsync(m => m.UserName == user_name && m.PasswordHash == password);

            //var result = await _signInManager.PasswordSignInAsync(user_name, password, false, lockoutOnFailure: true); Identity

            if (userInfo != null)
            {
                json.status = 0;
                json.msg = "登录成功";
                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
                };

                await HttpContext.SignInAsync(userInfo.Id.ToString(), userInfo.UserName);
                return new JsonResult(json);
            }
            else
            {
                json.status = -1;
                json.msg = "帐户或者密码不正确";
            }
            return new JsonResult(json);
        }


        [HttpGet]
        public ActionResult GetAuthCode()
        {
            string pic_code = "";
            byte[] b = new VerifyCode().GetVerifyCode(ref pic_code);
            HttpContext.Session.SetString("pic_code", pic_code);//存入session
            return File(b, @"image/Gif");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();
            return Redirect("/account/login");
        }

    }
}
