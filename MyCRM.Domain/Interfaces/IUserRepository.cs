using Microsoft.AspNetCore.Http;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.Interfaces
{
    public interface IUserRepository
    {
        //User
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task<User> GetUserWithID(long userId);

        //Marketer
        Task AddMarketerAsync(Marketer marketer);
        Task UpdateMarketerAsync(Marketer marketer);
        //Filter
        Task<FilterUserViewModel> filterUser(FilterUserViewModel filterUser); 

        Task SaveChangesAsync();
    }
}
