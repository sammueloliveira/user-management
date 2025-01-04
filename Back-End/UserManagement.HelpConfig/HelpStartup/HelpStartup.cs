using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.Services;
using UserManagement.Domain.ServicesInterfaces;
using UserManagement.Infra.Repositories;

namespace UserManagement.HelpConfig.HelpStartup
{
    public static class HelpStartup
    {
        public static void ConfigureScoped(IServiceCollection services)
        {
            // INTERFACE - REPOSITORY

            services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));
            services.AddScoped<IUser, UserRepository>();

            // DOMAIN SERVICES

            services.AddScoped<IUserService, UserService>();


        }
    }
}
