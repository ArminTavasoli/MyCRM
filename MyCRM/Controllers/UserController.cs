﻿using Microsoft.AspNetCore.Mvc;
using MyCRM.Application.Interfaces;
using MyCRM.Domain.ViewModel.User;

namespace MyCRM.Controllers
{
    public class UserController : Controller
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
        public async Task<IActionResult> CreateMarketer(AddMarketerViewModel addMarketer)
        {
            if (!ModelState.IsValid)
            {
                return View(addMarketer);
            }

            var result = await _userServices.AddMarketer(addMarketer);

            switch (result)
            {
                case AddMarketerResult.Success:
                    return RedirectToAction("Index");
                case AddMarketerResult.Fail:
                    ModelState.AddModelError("UserName", "مشکلی در ثبت اطلاعات می باشد...");
                    break;
            }

            return View(addMarketer);
        }

        #endregion Create User
    }
}
