using BraintreeSample.APIHelper.Builders;
using BraintreeSample.API.Controllers;
using BraintreeSample.API.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;

namespace BraintreeSample.API.Middleware
{
    public class AFAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserService _userService = (UserService)filterContext.HttpContext.RequestServices.GetService(typeof(UserService));
            var context = filterContext.HttpContext;
            var headers = context.Request.Headers;
            if (headers.ContainsKey("Authorization"))
            {
                var authHeader = headers["Authorization"].ToString();
                var bearertoken = authHeader.Substring("Basic ".Length).Trim();
                var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(bearertoken));
                var credentials = credentialstring.Split(':');

                if (credentials.Length < 2)
                    context.Response.StatusCode = 401;

                var username = credentials[0];
                var token = credentials[1];
                var user = _userService.GetValidUserByToken(username, token);
                if (user.Errors.Count > 0)
                    context.Response.StatusCode = 401;
                else
                    ((BaseController)filterContext.Controller).CurrentUser = user.Data;
            }
            else
                context.Response.StatusCode = 401;
        }
    }
}
