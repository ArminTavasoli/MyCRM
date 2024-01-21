using MyCRM.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.Interfaces
{
    public interface IUserService
    {
        Task<AddMarketerResult> AddMarketer(AddMarketerViewModel  marketer); 

        Task<FilterUserViewModel> Filter (FilterUserViewModel filter);
    }
}
