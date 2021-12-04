using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Auth.Attributes
{
    public class TransactionAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Transactions { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(Transactions))
            {
                return;
            }

            var svc = context.HttpContext.RequestServices;
            var _session = svc.GetService(typeof(Core.JWT.ISession)) as Core.JWT.ISession;
            if (_session.HasUser() && _session.HasPermission(Transactions.Split(","))) 
            {
                return;
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
