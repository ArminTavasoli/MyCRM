using Microsoft.EntityFrameworkCore;
using MyCRM.Data.DbContexts;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.Interfaces;
using MyCRM.Domain.ViewModel.Paging;
using MyCRM.Domain.ViewModel.User;

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
        #region User
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUserWithID(long userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserDetailById(long userId)
        {
            return await _context.Users
                .Include(u => u.Marketer)
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(a => a.UserID == userId);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        #endregion



        //Marketer
        #region Marketer
        public async Task AddMarketerAsync(Marketer marketer)
        {
            await _context.Marketers.AddAsync(marketer);
        }

        public async Task UpdateMarketerAsync(Marketer marketer)
        {
            _context.Marketers.Update(marketer);
        }


        public async Task<Marketer> GetMarketerById(long marketerId)
        {
            return await _context.Marketers.FirstOrDefaultAsync(m => m.UserID == marketerId);
        }


        //Filter User
        public async Task<FilterUserViewModel> filterUser(FilterUserViewModel filterUser)
        {
            var query = _context.Users
                        .OrderByDescending(a => a.CreatedDate)
                        .Include(a => a.Marketer)
                        .Include(a => a.Customer)
                        .Where(a => !a.IsDeleted)
                        .AsQueryable();

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
        #endregion


        //Customer
        #region Customer
        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer> GetCustomerWithId(long customerId)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstAsync(c => c.UserID == customerId);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
        }
        #endregion

        //Save
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
