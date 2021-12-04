using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Command.RoleCommand
{
    public class StoreRoleCommand : IRequest<bool>
    {
        public string TypeRole { get; set; }

        public string Descripcion { get; set; }

    }
}
