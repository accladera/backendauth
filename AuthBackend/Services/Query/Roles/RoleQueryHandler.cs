using Domain.Entities;
using MediatR;
using Services.Repository.RoleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Query.RolesQuery
{
    public class RoleQueryHandler :
           IRequestHandler<ListRolesQuery, List<Role>>

    {
        private readonly IRoleRepository _repository;

        public RoleQueryHandler(
           IRoleRepository roleRepository)
        {
            _repository = roleRepository;
        }

        public async Task<List<Role>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.getListRole();
        }

      
    }
}