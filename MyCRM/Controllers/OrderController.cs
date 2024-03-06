using Microsoft.AspNetCore.Mvc;
using MyCRM.Application.Extensions;
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
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel orderViewModel, IFormFile? orderImage)
        {
            ViewBag.customer = await _userService.GetCustomerById(orderViewModel.CustomerId);

            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "خطایی رخ داده است ...";
                return View(orderViewModel);
            }

            var result = await _orderService.CreateOrder(orderViewModel, orderImage);

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

        #region Edite Order
        //Fill Order for Edite
        public async Task<IActionResult> EditeOrder(long orderId)
        {
            var result = await _orderService.FillEditeOrderModel(orderId);
            ViewBag.customer = await _userService.GetCustomerById(result.CustomerId);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditeOrder(EditeOrderViewModel editeOrder, IFormFile orderImage)
        {
            ViewBag.customer = await _userService.GetCustomerById(editeOrder.CustomerId);

            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد...";
                return View(editeOrder);
            }

            var result = await _orderService.EditeOrder(editeOrder, orderImage);

            switch (result)
            {
                case EditeOrderResult.Success:
                    TempData[SuccessMessage] = "سفارش با موفقیت ویرایش شد...";
                    return RedirectToAction("FilterOrders");
                case EditeOrderResult.Fail:
                    TempData[ErrorMessage] = "ویرایش سفارش با خطا مواجه شده است ...";
                    break;
            }

            return View(editeOrder);
        }
        #endregion


        #region Delete Order
        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            var result = await _orderService.DeleteOrder(orderId);
            if (result)
            {
                TempData[SuccessMessage] = "سفارش مورد نظر با موفقیت پاک شد...";
                return RedirectToAction("FilterOrders");
            }

            TempData[ErrorMessage] = "حذف سفارش با خطا رو به رو شد ...";
            return RedirectToAction("FilterOrders");
        }
        #endregion


        #region Order Select Marketer
        public async Task<IActionResult> SelectMarketerModal(long orderId)
        {
            var model = new OrderSelectMarketerViewModel()
            {
                OrderId = orderId
            };

            ViewBag.Marketers = await _userService.GetMarketerList();
            return PartialView("_SelectMarketerModalPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> SelectMarketerModal(OrderSelectMarketerViewModel selectModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { status = "NotValid" });
            }

            var result = await _orderService.AddOrderSelectMarketer(selectModel, User.GetUserId());

            switch (result)
            {
                case AddOrderSelectMarketerResult.Success:
                    return new JsonResult(new { status = "Success" });
                case AddOrderSelectMarketerResult.Fail:
                    return new JsonResult(new { status = "Error" });
                case AddOrderSelectMarketerResult.SelectMarketerExist:
                    return new JsonResult(new { status = "MarketerExist" });
            }
            return new JsonResult(new { status = "Error"});
        }
        #endregion
    }
}
