using Domain.Entities;
using MediatR;
using Service.Models;
//using Service.Models;
using Service.Utils;
using System;

namespace Service.Command.UserCommand
{
    public class LoginCommand: IRequest<LoginResponseModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
