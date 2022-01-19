using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Helper
{
    public class SessionTokenFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tokenVariable = context.HttpContext.Session.GetString("token");
            if(tokenVariable == null || tokenVariable == string.Empty)
            {
                RouteValueDictionary redirectTargetDictionary = new(new { action = "Login", controller = "Account"});
                context.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
        }
    }
}
