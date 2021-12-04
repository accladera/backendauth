using Core.BussinesRule;
using Core.Rules;
using Data.Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public User Add(User entity)
        {
            return AddAux(entity);
        }

        public User Update(User entity)
        {
            return UpdateAux(entity);
        }

        public async Task<User> getUserById(int id)
        {
            return await _context.Users.Where(ele => ele.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserEmailAsync(string email)
        {
            var record = await _context.Users
                .SingleOrDefaultAsync(s => s.Email == email);

            return record;
        }

        public async Task<bool> GetUserEmailExistAsync(string email)
        {
             var record = await _context.Users
                .SingleOrDefaultAsync(s => s.Email == email);
            if (record != null)
                return true;
            return false;
        }
    }
}