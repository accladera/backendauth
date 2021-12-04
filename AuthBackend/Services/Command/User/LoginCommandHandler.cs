
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Models;
using Service.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Messaging.Send.Sender;
using Services.Repository;
using Core.JWT;
using Services.Command.UserCommand;

namespace Service.Command.UserCommand
{

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseModel>,
                                        IRequestHandler<StoreUserClientCommand, int>,
                                        IRequestHandler<StroreUserGerenteCommand, int>


    {
        private readonly IUserRepository _repository;
        private User _user;
        private readonly IConfiguration _config;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IConfiguration configuration
             ) : base()
        {
            _repository = userRepository;
            _config = configuration;
        }

        protected async Task<User> Validate(LoginCommand request)
        {
            _user = await _repository.GetUserEmailAsync(request.Email);
            if (_user != null)
            {
                if (!_user.authenticate(request.Password))
                {
                    throw new InvalidOperationException("Las credenciales son incorrectas");
                }
                return _user;
            }
            else
            {
                throw new InvalidOperationException("No existe un usuario con las credenciales proporcionadas");
            }
        }

        public async Task<LoginResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var users = await Validate(request);


            var loginResponseModel = new LoginResponseModel();

           
            var token = JWTManager.GenerateToken(_config["Jwt:Key"], _user);

            loginResponseModel.Token = token;
            loginResponseModel.ResetPassword = users.ResetPassword;
            loginResponseModel.UserId=users.Id;
            loginResponseModel.ResetPassword = users.ResetPassword;
            loginResponseModel.RoleId = users.RoleId;

            //_cache.Store($"jwt_{token}", loginResponseModel);

            return loginResponseModel;
        }

        public async Task<int> Handle(StoreUserClientCommand request, CancellationToken cancellationToken)
        {
            User user = new User();

            bool emailExist = await _repository.GetUserEmailExistAsync(request.Email);

            if(emailExist)
                throw new InvalidOperationException("Correo Proporcionado ya esta usado");

            user.CreateUserClient(request.Name, request.Password,request.Email,request.BirthDate);

            this._repository.Add(user);
            await this._repository.UnitOfWork.SaveEntitiesAsync();

            return user.Id;
        }

        public async Task<int> Handle(StroreUserGerenteCommand request, CancellationToken cancellationToken)
        {
            User user = new User();

            bool emailExist = await _repository.GetUserEmailExistAsync(request.EmailUser);

            if (emailExist)
                throw new InvalidOperationException("Correo Proporcionado ya esta usado");

            user.CreateUserGerent(request.NameUser, request.Password, request.EmailUser);

            this._repository.Add(user);
            await this._repository.UnitOfWork.SaveEntitiesAsync();

            return user.Id;
        }
    }
}
