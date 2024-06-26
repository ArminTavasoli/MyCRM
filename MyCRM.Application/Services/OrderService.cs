﻿using Azure;
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


        #region Filter and Paging Order
        public async Task<FilterOrderViewModel> FilterOrder(FilterOrderViewModel filterOrder)
        {
            var query = await _orderRepository.GetOrders();

            #region Filter
            query = query.Where(o => !o.IsDelete);

            if (!string.IsNullOrEmpty(filterOrder.FilterCustomerName))
            {
                query = query.Where(a =>
                EF.Functions.Like(a.Customer.User.FirstName, $"%{filterOrder.FilterCustomerName}%") ||
                EF.Functions.Like(a.Customer.User.LastName, $"%{filterOrder.FilterCustomerName}%") ||
                EF.Functions.Like(a.Customer.User.MobilePhone, $"%{filterOrder.FilterCustomerName}%"));
            }

            if (!string.IsNullOrEmpty(filterOrder.FilterOrderName))
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
        #endregion

        #region Create Order
        //Add Order
        public async Task<CreateOrderResult> CreateOrder(CreateOrderViewModel createOrderViewModel, IFormFile? imageProfile)
        {
            var imageProfileName = "";

            if (imageProfile?.Length > 0)
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

            if (!string.IsNullOrEmpty(imageProfileName))
            {
                order.ImageName = imageProfileName;
            }

            await _orderRepository.AddOrder(order);
            await _orderRepository.SaveChange();

            return CreateOrderResult.Success;
        }
        #endregion

        #region Edite Order
        //Fill Order for Edite
        public async Task<EditeOrderViewModel> FillEditeOrderModel(long orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return null;
            }

            var result = new EditeOrderViewModel()
            {
                OrderId = orderId,
                CustomerId = order.CustomerId,
                Title = order.Title,
                Description = order.Description,
                ImageName = order.ImageName
            };

            return result;
        }

        //Edite Order Final
        public async Task<EditeOrderResult> EditeOrder(EditeOrderViewModel editeOrder, IFormFile orderImage)
        {
            var order = await _orderRepository.GetOrderById(editeOrder.OrderId);

            if (order == null)
            {
                return EditeOrderResult.Fail;
            }

            var orderImageName = "";

            if (orderImage?.Length > 0)
            {
                orderImageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(orderImage.FileName);
                orderImage.AddImageToServer(orderImageName, FilePath.OrderImagePathServer, 280, 280);
            }

            order.Title = editeOrder.Title;
            order.Description = editeOrder.Description;
            order.OrderType = editeOrder.OrderType;

            if (string.IsNullOrEmpty(orderImageName))
            {
                order.ImageName = orderImageName;
            }

            await _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChange();

            return EditeOrderResult.Success;
        }
        #endregion


        #region Delete Order
        public async Task<bool> DeleteOrder(long orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                return false;
            }

            order.IsDelete = true;

            await _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChange();

            return true;
        }
        #endregion


        #region Order select Marketer

        //Add Order Select Marketer
        public async Task<AddOrderSelectMarketerResult> AddOrderSelectMarketer(OrderSelectMarketerViewModel orderSelectMarketer , long userId)
        {
            var order = await _orderRepository.GetOrderById(orderSelectMarketer.OrderId);
            if (order == null)
            {
                return AddOrderSelectMarketerResult.Fail;
            }

            var selectMarketerQueryable = await _orderRepository.GetOrderSelectedMarketers();
            if(selectMarketerQueryable.Any(m => m.OrderId == order.OrderId && !m.IsDelete))
            {
                return AddOrderSelectMarketerResult.SelectMarketerExist;
            }

            var selectMarketer = new OrderSelectedMarketer()
            {
                OrderId = orderSelectMarketer.OrderId,
                Description = orderSelectMarketer.Description ,
                MarketerId = orderSelectMarketer.MarketerId ,
                ModifyUserId = userId

            };

            await _orderRepository.AddOrderSelectMarketer(selectMarketer);
            await _orderRepository.SaveChange();

            return AddOrderSelectMarketerResult.Success;
        }

        #endregion





    }
}
