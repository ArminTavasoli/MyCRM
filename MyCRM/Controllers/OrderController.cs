using Microsoft.AspNetCore.Mvc;
using MyCRM.Application.Interfaces;

namespace MyCRM.Controllers
{
    public class OrderController : Controller
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

        /*       public async Task<IActionResult> CreateOrder(long Id)
               {
                   ViewBag.customer = await _userService.Get
               }*/
    }
}
