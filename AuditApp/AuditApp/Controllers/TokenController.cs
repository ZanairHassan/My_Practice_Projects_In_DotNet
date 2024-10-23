using BLL.Interfaces;
using DAL.DBContext;
using DAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public TokenController(ITokenService tokenService, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        [Route("CreateToken")]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] TokenVM tokenVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoggingUtlity.ExLog(ModelState.ToString(), _serviceProvider);
                    return BadRequest(ModelState);
                }
                await _tokenService.CreateToken(tokenVM);
                LoggingUtlity.LogText("CreateToken: Token Created", _configuration);
                return Ok("Token Created");
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [Route("DeleteToken/{tokenID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteToken(int tokenID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoggingUtlity.ExLog(ModelState.ToString(), _serviceProvider);
                    return BadRequest(ModelState);
                }
                var deleteToken = await _tokenService.DeleteToken(tokenID);
                LoggingUtlity.LogText($"DeleteToken: Token with ID {tokenID} deleted", _configuration);
                return Ok(deleteToken);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [Route("GetAllToken")]
        [HttpGet]
        public async Task<IActionResult> GetAllToken()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoggingUtlity.ExLog(ModelState.ToString(), _serviceProvider);
                    return BadRequest(ModelState);
                }
                var allToken = await _tokenService.GetAllToken();
                LoggingUtlity.LogText("GetAllToken: Retrieved all tokens", _configuration);
                return Ok(allToken);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [Route("GetToken/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetToken(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoggingUtlity.ExLog(ModelState.ToString(), _serviceProvider);
                    return BadRequest(ModelState);
                }
                var token = await _tokenService.GetToken(id);
                LoggingUtlity.LogText($"GetToken: Retrieved token with ID {id}", _configuration);
                return Ok(token);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [Route("UpdateToken/{tokenID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateToken(int tokenID, TokenVM tokenVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoggingUtlity.ExLog(ModelState.ToString(), _serviceProvider);
                    return BadRequest(ModelState);
                }
                var updateToken = await _tokenService.UpdateToken(tokenID, tokenVM);
                LoggingUtlity.LogText($"UpdateToken: Token with ID {tokenID} updated", _configuration);
                return Ok(updateToken);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }
    }
}
