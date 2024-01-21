using Microsoft.EntityFrameworkCore;
using MyCRM.Data.DbContexts;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.Interfaces;
using MyCRM.Domain.ViewModel.Paging;
using MyCRM.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CrmContext _context;

        public UserRepository(CrmContext context)
        {
            this._context = context;
        }





        //User
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUserWithID(long userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        //Marketer
        public async Task AddMarketerAsync(Marketer marketer)
        {
            await _context.Marketers.AddAsync(marketer);
        }

        public async Task UpdateMarketerAsync(Marketer marketer)
        {
            _context.Marketers.Update(marketer);
        }

        //Save
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        //Filter User
        public async Task<FilterUserViewModel> filterUser(FilterUserViewModel filterUser)
        {
            var query = _context.Users.AsQueryable();

            #region Filtering
            //Filter FirstName
            if (!string.IsNullOrEmpty(filterUser.FilterName))
            {
                query = query.Where(a => EF.Functions.Like(a.FirstName, $"%{filterUser.FilterName}%"));
            }

            //Filter LastName
            if (!string.IsNullOrEmpty(filterUser.FilterLastName))
            {
                query = query.Where(l => EF.Functions.Like(l.LastName, $"%{filterUser.FilterLastName}%"));
            }

            //Filter MobilePhone
            if (!string.IsNullOrEmpty(filterUser.FilterMobile))
            {
                query = query.Where(m => EF.Functions.Like(m.MobilePhone, $"%{filterUser.FilterMobile}%"));
            }
            #endregion

            #region Paging
            var pager = Pager.BuildPager(filterUser.PageId, filterUser.HowManyShowPageAfterAndBefore, filterUser.AllEntitiesCount,
                                                    filterUser.TakeEntity);
            var allEntites = await query.Paging(pager).ToListAsync();
            #endregion


            return filterUser.SetEntity(allEntites).SetPaging(pager);
        }
    }
}
