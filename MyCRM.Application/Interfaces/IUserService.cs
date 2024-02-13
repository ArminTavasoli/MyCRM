using Microsoft.AspNetCore.Http;
using MyCRM.Domain.Entities.Account;
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
        //Marketer
        Task<AddMarketerResult> AddMarketer(AddMarketerViewModel  marketer, IFormFile imageProfile);
        Task<EditeMarketerViewModle> GetMarketerforEdite(long UserId);
        Task<EditeMarketerResult> EditeMarketer(EditeMarketerViewModle editeMarketer, IFormFile imageProfile);

        //Filter
        Task<FilterUserViewModel> Filter (FilterUserViewModel filter);
        
        
        //Cusetomer
        Task<AddCustomerResult> AddCustomer(AddCustomerViewModel newCustomer , IFormFile imageProfile);
        Task<EditeCustomerViewModel> FillEditeCustomerViewModel(long UserId);
        Task<EditeCustomerResult> EditeCustomer(EditeCustomerViewModel editeCustomer , IFormFile imageProfile);
        Task<Customer> GetCustomerById(long CustomerId);

    }
}
