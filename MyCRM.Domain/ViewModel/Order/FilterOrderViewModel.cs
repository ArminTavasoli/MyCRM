using MyCRM.Domain.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.Order
{
    public class FilterOrderViewModel : BasePaging
    {
        public List<Entities.Orders.Order>? Orders { get; set; }
        public string? FilterOrderName { get; set; }
        public string? FilterCustomerName{ get; set; }

        public FilterOrderViewModel SetEntity(List<Entities.Orders.Order> orders)
        {
            this.Orders = orders;
            return this;
        }

        public FilterOrderViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.SkipEntity = paging.SkipEntity;
            this.StartPage = paging.StartPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.PageCount = paging.PageCount;
            this.TakeEntity = paging.TakeEntity;

            return this;
        }
    }
}
