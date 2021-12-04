using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Query.RolesQuery
{
    public class ListRolesQuery : IRequest<List<Role>>
    {
       
    }
}