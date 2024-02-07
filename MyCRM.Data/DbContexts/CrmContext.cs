using Microsoft.EntityFrameworkCore;
using MyCRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Data.DbContexts
{
    public class CrmContext : DbContext
    {
        public CrmContext(DbContextOptions<CrmContext> options ) : base (options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Marketer> Marketers { get; set; }

        public async void FixData()
        {
            var userToChange =await Users.FirstOrDefaultAsync(u => u.CreatedDate == DateTime.MinValue);
            if (userToChange != null)
            {
                userToChange.CreatedDate = DateTime.Now;
            }

           await SaveChangesAsync();
        }
    }
}
