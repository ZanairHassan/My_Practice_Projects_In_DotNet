using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BLL
{
    public static class Common
    {
        public static string ReturnBadRequest(ModelStateDictionary ModelState, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            LoggingUtlity.LogText("Model State is not valid", configuration);
            LoggingUtlity.LogText(ModelState.ToString(), configuration);
            LoggingUtlity.ExLog(ModelState.ToString(), serviceProvider);
            LoggingUtlity.LogText("Returning Bad Request", configuration);
            return ModelState.ToString();
        }

        public static async Task<bool> ValidateToken(string token, ITokenService tokenService, IConfiguration configuration)
        {
            LoggingUtlity.LogText($"Your Token is {token}", configuration);
            bool isTokenValid = false;
            if (string.IsNullOrEmpty(token))
            {
                LoggingUtlity.LogText("Token is Empty", configuration);
                isTokenValid = false;
            }

            var jwttoken = await tokenService.GetTokenByjwtToken(token);
            if (jwttoken == null)
            {
                LoggingUtlity.LogText("Token is not valid", configuration);
                isTokenValid = false;
            }
            else
            {
                LoggingUtlity.LogText("Token is valid", configuration);
                isTokenValid = true;
            }

            return isTokenValid;
        }

        
    }
}
