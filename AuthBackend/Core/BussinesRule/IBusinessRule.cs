using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BussinesRule
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
