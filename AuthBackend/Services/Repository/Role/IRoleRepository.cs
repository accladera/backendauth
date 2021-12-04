using Core.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.RoleRepository
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> getRoleById(int id);
        Task<List<Role>> getListRole();

    }
}
