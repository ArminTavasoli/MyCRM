using Microsoft.AspNetCore.Http;
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
        Task<AddMarketerResult> AddMarketer(AddMarketerViewModel  marketer, IFormFile imageProfile); 

        Task<FilterUserViewModel> Filter (FilterUserViewModel filter);

        Task<EditeMarketerViewModle> GetMarketerforEdite(long UserId);

        Task<EditeMarketerResult> EditeMarketer(EditeMarketerViewModle editeMarketer , IFormFile imageProfile);

        Task<AddCustomerResult> AddCustomer(AddCustomerViewModel newCustomer , IFormFile imageProfile);

    }
}
