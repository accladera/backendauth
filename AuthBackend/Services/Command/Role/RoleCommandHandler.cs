using Domain.Entities;
using MediatR;
using Services.Repository.RoleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Command.RoleCommand
{
    public class RoleCommandHandler : 
                                        IRequestHandler<StoreRoleCommand, bool>


    {
        private readonly IRoleRepository _repository;

        public RoleCommandHandler(
            IRoleRepository userRepository
             ) : base()
        {
            _repository = userRepository;
        }

        public async Task<bool> Handle(StoreRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = new Role(request.Descripcion,request.TypeRole);
            this._repository.Add(role);
            await this._repository.UnitOfWork.SaveEntitiesAsync();
            return true;
        }
    }
}
