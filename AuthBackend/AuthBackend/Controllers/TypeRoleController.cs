using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Services.Query.RolesQuery;
using Services.Command.RoleCommand;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/auth/role")]
    [ApiController]

    public class TypeRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TypeRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }



        

        /// <summary>
        /// Devuelve lista de roles
        /// </summary>
        /// <returns>Listado de roles</returns>
        [HttpGet("list/")]
        // [TransactionAuthorize]
        [AllowAnonymous]
        public async Task<ActionResult> LoginController()
        {
            try
            {
                return Ok(await _mediator.Send(new ListRolesQuery()));
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
        [HttpPost("store")]
        // [TransactionAuthorize]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> StoreClientController(StoreRoleCommand model)
        {
            try
            {
                await _mediator.Send(model);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







    }




}