using Microsoft.AspNetCore.Http;
using MyCRM.Domain.Entities.Orders;
using MyCRM.Domain.ViewModel.Order;
using MyCRM.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(long orderIds);

        Task<IQueryable<Order>> GetOrders();

        Task AddOrder(Order order);

        Task UpdateOrder(Order order);

        Task SaveChange();
    }
}
