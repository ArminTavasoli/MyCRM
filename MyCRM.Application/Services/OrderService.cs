using Microsoft.AspNetCore.Http;
using MyCrm.Application.Extensions;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Security;
using MyCRM.Application.StaticTools;
using MyCRM.Domain.Entities.Orders;
using MyCRM.Domain.Interfaces;
using MyCRM.Domain.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.Services
{
    public class OrderService : IOrderService
    {
        #region Constructor
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository order)
        {
            this._orderRepository = order;
        }
        #endregion

        //Add Order
        public async Task<CreateOrderResult> CreateOrder(CreateOrderViewModel createOrderViewModel, IFormFile imageProfile)
        {
            var imageProfileName = "";

            if(imageProfile?.Length > 0)
            {
                imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);
            }

            var order = new Order()
            {
                CustomerId = createOrderViewModel.CustomerId,
                Description = createOrderViewModel.Description,
                Title = createOrderViewModel.Title,
                OrderType = createOrderViewModel.OrderType
            };

            if(!string.IsNullOrEmpty(imageProfileName) )
            {
                order.ImageName = imageProfileName;
            }

            await _orderRepository.AddOrder(order);
            await _orderRepository.SaveChange();

            return CreateOrderResult.Success;
        }
    }
}
