using MyCRM.Domain.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.User
{
    public class FilterUserViewModel : BasePaging
    {
        public string FilterName { get; set; } = string.Empty;
        public string FilterLastName { get; set; } = string.Empty;
        public string FilterMobile { get; set; } = string.Empty;

        public List<Entities.Account.User> Users { get; set; }


        public FilterUserViewModel SetEntity(List<Entities.Account.User> users)
        {
            this.Users = users;
            return this;    
        }

        public FilterUserViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.PageCount = paging.PageCount;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;

            return this;
        }
    }
}
