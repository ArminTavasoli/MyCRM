using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyCRM.Application.Interfaces;
using MyCRM.Domain.ViewModel.Account;
using System.Security.Claims;

namespace MyCRM.Controllers
{
    public class AccountController : Controller
    {
        #region Ctor
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }
        #endregion

        #region Login
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login(LoginUserViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                TempData["WarningMessage"] = "اطلاعات وارد شده معتبر نمی باشد...";
                return View(loginViewModel);
            }

            var result = await _userService.LoginUser(loginViewModel);

            switch(result)
            {
                case LoginUserResult.Success:
                    var loginUser = _userService.GetUserByUserName(loginViewModel.UserName);

                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier , loginUser.Id.ToString())
                    };

                    var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = loginViewModel.RememberMe
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    TempData["SuccessMessage"] = "ورود با موفقیت انجام شد ...";

                   return RedirectToAction("Index" , "Home");                  

                case LoginUserResult.NotFound:
                    TempData["ErrorMessage"] = "حساب کاربری با این اطلاعات یافت نشد...";
                    break;

                case LoginUserResult.PasswordNotCorrect:
                    TempData["ErrorMessage"] = "رمز کاربری نادرست است ...";
                    break;
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Logout
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData["SuccessMessage"] = "خروج با موفقیت انجام شد .. ";
            return RedirectToAction("Login");
        }
        #endregion
    }
}
