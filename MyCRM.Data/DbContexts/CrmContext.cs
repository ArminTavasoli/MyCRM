using Microsoft.EntityFrameworkCore;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.Entities.Orders;
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

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderSelectedMarketer> OrderSelectedMarketers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderSelectedMarketer>()
                .HasOne(o => o.Order)
                .WithOne(o => o.OrderSelectedMarketer)
                .HasForeignKey<OrderSelectedMarketer>(o => o.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderSelectedMarketer>()
               .HasOne(o => o.Marketer)
               .WithMany(o => o.OrderSelectedMarketers)
               .HasForeignKey(o => o.MarketerId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderSelectedMarketer>()
              .HasOne(o => o.ModifyUser)
              .WithMany(o => o.OrderSelectedMarketers)
              .HasForeignKey(o => o.ModifyUserId)
              .OnDelete(DeleteBehavior.Restrict);

        }



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
