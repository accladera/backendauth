using Core.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> getUserById(int id);

        Task<User> GetUserEmailAsync(string email);

        Task<bool> GetUserEmailExistAsync(string email);

    }
}
