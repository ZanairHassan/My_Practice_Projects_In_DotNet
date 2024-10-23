using BLL.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuditApp
{
    public class AuthenticateURL : IAsyncActionFilter
    {
        private readonly IAllowedURLService _allowedURLService;

        public AuthenticateURL(IAllowedURLService allowedURLService)
        {
            _allowedURLService = allowedURLService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestIpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                var urlObject = await _allowedURLService.GetAllowedUrlByUrl(requestIpAddress);
                if (urlObject == null)
                {
                    await _allowedURLService.AddDeclinedIP(requestIpAddress);
                    context.Result = new RedirectResult("https://askaii.co.uk");
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
