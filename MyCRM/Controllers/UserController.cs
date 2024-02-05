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



        #region User List
        [HttpGet]
        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            var result = await _userServices.Filter(filter);
            return View(result);
        }
        #endregion

        #region Create User
        //Create Customer
        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(AddCustomerViewModel customerViewModel , IFormFile? imageProfile )
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "خطایی رخ داده است ...";
                foreach(var error in ModelState.Values.SelectMany(e => e.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(customerViewModel);
            }

            var customer = await _userServices.AddCustomer(customerViewModel, imageProfile); 

            switch(customer)
            {
                case AddCustomerResult.Success:
                    TempData[SuccessMessage] = "موفقیت...";
                    return RedirectToAction("Index"); 
                case AddCustomerResult.Fail:
                    TempData[ErrorMessage] = "خطا ";
                    break;
            }

            return View(customerViewModel);
        }


        //Create Marketer
        public async Task<IActionResult> CreateMarketer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarketer(AddMarketerViewModel addMarketer, IFormFile? imageProfile)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "خطایی رخ داده است...";
                return View(addMarketer);               
            }

            var result = await _userServices.AddMarketer(addMarketer, imageProfile) ;

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


        #region EditeMarketer
        [HttpGet]
        public async Task<IActionResult> EditeMarketer(long Id)
        {
            var marketer = await _userServices.GetMarketerforEdite(Id);
            if (marketer == null)
            {
                return NotFound();
            }

            return View(marketer);
        }

        [HttpPost]
        public async Task<IActionResult> EditeMarketer(EditeMarketerViewModle editeMarketer, IFormFile imageProfile)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده کامل نمی باشد...";
                return View(editeMarketer);
            }

            var result = await _userServices.EditeMarketer(editeMarketer, imageProfile);

            switch (result)
            {
                case EditeMarketerResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ...";
                    return RedirectToAction("Index");
                case EditeMarketerResult.Faild:
                    TempData[ErrorMessage] = "عملیات با خطا مواجه شد ...";
                    break;
            }

            return View(editeMarketer);

        }
        #endregion
    }
}
