using Microsoft.AspNetCore.Mvc;
using MyCRM.Application.Interfaces;
using MyCRM.Domain.ViewModel.User;

namespace MyCRM.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userServices;

        #region Constractor
        public UserController(IUserService userService)
        {
            this._userServices = userService;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            var result = await _userServices.Filter(filter);
            return View(result);
        }

        #region Create User
        //Create Customer
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }


        //Create Marketer
        public async Task<IActionResult> CreateMarketer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarketer(AddMarketerViewModel addMarketer , IFormFile imageProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(addMarketer);
            }

            var result = await _userServices.AddMarketer(addMarketer , imageProfile);

            switch (result)
            { 
                case AddMarketerResult.Success:
                    TempData[SuccessMessage] = "موفقیت";
                    return RedirectToAction("Index");
                case AddMarketerResult.Fail:
                    TempData[WarningMessage] = "خطا";   
                    ModelState.AddModelError("UserName", "مشکلی در ثبت اطلاعات می باشد...");
                    break;
            }

            return View(addMarketer);
        }

        #endregion Create User
    }
}
