using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Utilities;
using BLL;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp
{
    public class AuthenticateToken : IAsyncActionFilter
    {
        private readonly ITokenService _tokenService;

        public AuthenticateToken(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            try
            {
                bool isVlaidToken = await Common.ValidateToken(token, _tokenService, null);
                if (!isVlaidToken)
                {
                    Utilities.LoggingUtlity.LogText("Token is InValid", null);

                    context.Result = new RedirectToActionResult("AuthenticateToken", "NotAllowed", null);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
            await next();
        }
    }
}
