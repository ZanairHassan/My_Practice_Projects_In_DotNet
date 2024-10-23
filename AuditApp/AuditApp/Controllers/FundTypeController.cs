
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class FundTypeController : ControllerBase
    {
        private readonly IFundTypeService _fundTypeService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public FundTypeController(
            IFundTypeService fundTypeService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _fundTypeService = fundTypeService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }
        
        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateFundType")]
        [HttpPost]
        public async Task<IActionResult> CreateFundType(FundTypeVM fundTypeVM)
        {
            LoggingUtlity.LogText("CreateFundTypesDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                var createFundType = await _fundTypeService.GetFundTypeByName(fundTypeVM.FundTypeName);
                if (createFundType == null)
                {
                   var fundType= await _fundTypeService.CreateFundType(fundTypeVM);
                    if (!string.IsNullOrEmpty(fundType.SuccessMessage))
                    {
                        LoggingUtlity.LogText(fundType.SuccessMessage, _configuration);
                    }
                    else
                    {
                        LoggingUtlity.LogText(fundType.ErrorMessage, _configuration);
                    }
                    return Ok(fundType);
                }
                else
                {
                    LoggingUtlity.LogText("Fund type already existed", _configuration);
                    return Conflict("existed");
                }
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteFundType/{fundtypeID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFundType(int fundtypeID)
        {
            LoggingUtlity.LogText("Deleting FundTypeDetails IsCalled", _configuration);
            try
            {
                var deleteFundType = await _fundTypeService.DeleteFundType(fundtypeID);
                LoggingUtlity.LogText("Fund Type Deleted", _configuration);
                return Ok(deleteFundType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllFundType")]
        [HttpGet]
        public async Task<IActionResult> GetAllFundType()
        {
            LoggingUtlity.LogText("Fetching All Funds Details", _configuration);
            try
            {
                var allFundType = await _fundTypeService.GetAllFundType();
                LoggingUtlity.LogText("GetAllFundType: Retrieved all FundTypes", _configuration);
                return Ok(allFundType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetFundType/{fundtypeID}")]
        [HttpGet]
        public async Task<IActionResult> GetFundType(int fundtypeID)
        {
            try
            {
                var fundType = await _fundTypeService.GetFundType(fundtypeID);
                LoggingUtlity.LogText($"GetFundType: Retrieved FundType with ID {fundtypeID}", _configuration);
                return Ok(fundType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateFundType/{fundtypeID}")]
        [HttpPut]
        public async Task<IActionResult> UpdateFundType(int fundtypeID, [FromBody] FundTypeVM fundTypeVM)
        {
            LoggingUtlity.LogText("UpdateFund type IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                var updateFundType = await _fundTypeService.UpdateFundType(fundtypeID, fundTypeVM);
                if (!string.IsNullOrEmpty(updateFundType.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateFundType.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateFundType.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateFundType: Fund with I updated", _configuration);
                return Ok(updateFundType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] FundTypeBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Fund Types", _configuration);

            var deleteFundTypes = await _fundTypeService.BulkDelete(model.fundTypesId);
            LoggingUtlity.LogText("BulkDeleted Users", _configuration);

            return Ok(deleteFundTypes);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
