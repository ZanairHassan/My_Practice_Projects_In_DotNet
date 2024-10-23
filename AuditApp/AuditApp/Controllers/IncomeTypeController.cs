
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTypeController : ControllerBase
    {
        private readonly IIncomeTypeService _incomeTypeService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public IncomeTypeController(AuditAppDBContext context,
            IIncomeTypeService incomeTypeService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _incomeTypeService = incomeTypeService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateIncomeType")]
        [HttpPost]
        public async Task<IActionResult> CreateIncomeType(IncomeTypeVM incomeTypeVM)
        {
            LoggingUtlity.LogText("CreateincomeTypeDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating income Type", _configuration);
                var incomeType = await _incomeTypeService.CreateIncomeType(incomeTypeVM);
                if (!string.IsNullOrEmpty(incomeType.SuccessMessage))
                {
                    LoggingUtlity.LogText(incomeType.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(incomeType.ErrorMessage, _configuration);
                }
                return Ok(incomeType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteIncomeType/{incomeTypeID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteIncomeType(int incomeTypeID)
        {
            LoggingUtlity.LogText("Deleting Income type Details IsCalled", _configuration);
            try
            {
                var deleteIncomeTYpe = await _incomeTypeService.DeleteIncomeType(incomeTypeID);
                LoggingUtlity.LogText("Income ty Dpeeleted", _configuration);
                return Ok(deleteIncomeTYpe);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllIncomeType")]
        [HttpGet]
        public async Task<IActionResult> GetAllIncomeType()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All Income types Details", _configuration);

                var allIncomeTYpes = await _incomeTypeService.GetAllIncomeType();
                LoggingUtlity.LogText("GetAllIncomeTypess: Retrieved all ", _configuration);
                return Ok(allIncomeTYpes);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetIncomeType/{incomeTypeID}")]
        [HttpGet]
        public async Task<IActionResult> GetIncomeType(int incomeTypeID )
        {
            try
            {
                var incomeType = await _incomeTypeService.GetIncomeType(incomeTypeID);

                LoggingUtlity.LogText($"GetIncomeType: Retrieved income type with ID", _configuration);
                return Ok(incomeType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateIncomeType/{incomeTypeID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateIncomeType(int incomeTypeID,[FromBody] IncomeTypeVM incomeTypeVM)
        {
            LoggingUtlity.LogText("UpdateIncomeType IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating incomeType", _configuration);

                var updateIncomeType = await _incomeTypeService.UpdateIncomeType(incomeTypeID, incomeTypeVM);
                if (!string.IsNullOrEmpty(updateIncomeType.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateIncomeType.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateIncomeType.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateFund: Fund with ID has updated", _configuration);
                return Ok(updateIncomeType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] IncomeTypeBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Users", _configuration);

            var deleteIncomeTypes = await _incomeTypeService.BulkDelete(model.IncomeTypesId);
            LoggingUtlity.LogText("BulkDeleted deleteIncomeTypes", _configuration);

            return Ok(deleteIncomeTypes);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
