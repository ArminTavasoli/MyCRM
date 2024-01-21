using Microsoft.Extensions.DependencyInjection;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Services;
using MyCRM.Data.Repository;
using MyCRM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.IoC
{
    public class DependencyContainers
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserService , UserServices>();
        }
    }
}
