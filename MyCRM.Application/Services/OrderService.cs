using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyCrm.Application.Extensions;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Security;
using MyCRM.Application.StaticTools;
using MyCRM.Domain.Entities.Orders;
using MyCRM.Domain.Interfaces;
using MyCRM.Domain.ViewModel.Order;
using MyCRM.Domain.ViewModel.Paging;
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

        public async Task<FilterOrderViewModel> FilterOrder(FilterOrderViewModel filterOrder)
        {
            var query = await _orderRepository.GetOrders();

            #region Filter
            if(!string.IsNullOrEmpty(filterOrder.FilterCustomerName) )
            {
                query = query.Where(a =>
                EF.Functions.Like(a.Customer.User.FirstName, $"%{filterOrder.FilterCustomerName}%") ||
                EF.Functions.Like(a.Customer.User.LastName, $"%{filterOrder.FilterCustomerName}%") ||
                EF.Functions.Like(a.Customer.User.MobilePhone, $"%{filterOrder.FilterCustomerName}%"));
            }

            if(!string.IsNullOrEmpty(filterOrder.FilterOrderName) )
            {
                query = query.Where(a => EF.Functions.Like(a.Title, $"%{filterOrder.FilterOrderName}%"));
            }
            #endregion

            #region Paging
            var pager = Pager.BuildPager(filterOrder.PageId, filterOrder.HowManyShowPageAfterAndBefore, await query.CountAsync(), filterOrder.TakeEntity);

            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filterOrder.SetEntity(allEntities).SetPaging(pager); 
        }
    }
}
