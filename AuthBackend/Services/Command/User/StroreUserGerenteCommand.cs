using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Command.UserCommand
{
    public class StroreUserGerenteCommand : IRequest<int>
    {
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
        public string Password { get; set; }
        public string NameBussines { get; set; }
        public string EmailBussines { get; set; }
    }
}
