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

namespace Services.Repository.RoleRepository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DataBaseContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public Role Add(Role entity)
        {
            return AddAux(entity);
        }

        public Role Update(Role entity)
        {
            return UpdateAux(entity);
        }

        public async Task<Role> getRoleById(int id)
        {
            return await _context.Roles.Where(ele => ele.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Role>> getListRole()
        {
            return await _context.Roles.ToListAsync();

        }

    }
}