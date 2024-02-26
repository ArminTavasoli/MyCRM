using Microsoft.AspNetCore.Http;
using MyCRM.Domain.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.Interfaces
{
    public interface IOrderService
    {
        Task<CreateOrderResult> CreateOrder(CreateOrderViewModel createOrderViewModel , IFormFile imageProfile);
        Task<FilterOrderViewModel> FilterOrder(FilterOrderViewModel filterOrder);

        Task<EditeOrderViewModel> FillEditeOrderModel(long orderId);
        Task<EditeOrderResult> EditeOrder(EditeOrderViewModel editeOrder, IFormFile orderImage);
        Task<bool> DeleteOrder(long orderId);
    }
}
