using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Service.Command.UserCommand;
using Services.Command.UserCommand;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/auth/user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }



        

        /// <summary>
        /// Actualiza el usuario.
        /// </summary>
        /// <returns>Usuario</returns>
        [HttpPost("login/")]
        // [TransactionAuthorize]
        [AllowAnonymous]
        public async Task<ActionResult> LoginController(LoginCommand model)
        {
            try
            {
                return Ok(await _mediator.Send(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Crea un usuario Cliente
        /// </summary>
        /// <returns>True</returns>
        [HttpPost("store/client")]
        // [TransactionAuthorize]
        [AllowAnonymous]
        public async Task<ActionResult> StoreClientController(StoreUserClientCommand model)
        {
            try
            {
                return Ok(await _mediator.Send(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Crea un usuario Gerente
        /// </summary>
        /// <returns>True</returns>
        [HttpPost("store/gerent")]
        // [TransactionAuthorize]
        [AllowAnonymous]
        public async Task<ActionResult<int>> StoreGerentController(StroreUserGerenteCommand model)
        {
            try
            {
                return Ok(await _mediator.Send(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }




}
