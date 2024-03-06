using Microsoft.EntityFrameworkCore;
using MyCRM.Data.DbContexts;
using MyCRM.Domain.Entities.Orders;
using MyCRM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        #region Constructor
        private readonly CrmContext _context;

        public OrderRepository(CrmContext crmContext)
        {
            this._context = crmContext;
        }
        #endregion

        #region Order

        //Get Order with Id
        public async Task<Order> GetOrderById(long orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        //Add Order
        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        //Edite Order
        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }


        public async Task<IQueryable<Order>> GetOrders()
        {
            return _context.Orders
                .OrderByDescending(o => o.CreateDate)
                .Include(o => o.Customer)
                .ThenInclude(o => o.User).AsQueryable();
        }
        #endregion

        #region Order Select Marketer
        //Add Order Select Marketer
        public async Task AddOrderSelectMarketer(OrderSelectedMarketer orderSelectMarketer)
        {
            await _context.OrderSelectedMarketers.AddAsync(orderSelectMarketer);
        }

        //Get Order Select Markerter
        public async Task<IQueryable<OrderSelectedMarketer>> GetOrderSelectedMarketers()
        {
            return  _context.OrderSelectedMarketers.AsQueryable();
        }
        #endregion

        //Save
        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

    }
}
