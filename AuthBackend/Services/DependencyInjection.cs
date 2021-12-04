using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.Repository;
using Services.Repository.RoleRepository;
using System.Reflection;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            // Todos los repositorys
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            return services;
        }

    }
}
