using Microsoft.AspNetCore.Mvc;
using MyCRM.Application.Interfaces;
using MyCRM.Domain.ViewModel.Order;

namespace MyCRM.Controllers
{
    public class OrderController : BaseController
    {
        #region Constructor
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            this._orderService = orderService;
            this._userService = userService;
        }
        #endregion

        #region Filter Order
        public async Task<IActionResult> FilterOrders(FilterOrderViewModel filter)
        {
            var result = await _orderService.FilterOrder(filter);
            return View(result);
        }
        #endregion

        #region Create Order
        //Create Order
        public async Task<IActionResult> CreateOrder(long id)
        {
                ViewBag.customer = await _userService.GetCustomerById(id);

            if (ViewBag.customer == null)
            {
                return NotFound();
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel orderViewModel , IFormFile orderImage)
        {
            ViewBag.customer = await _userService.GetCustomerById(orderViewModel.CustomerId);

            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "خطایی رخ داده است ...";
                return View(orderViewModel);
            }

            var result = await _orderService.CreateOrder(orderViewModel , orderImage);

            switch (result)
            {
                case CreateOrderResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد...";
                    return RedirectToAction();
                case CreateOrderResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شده است...";
                    break;
            }

            return View(orderViewModel);
            
        }
        #endregion
    }
}
