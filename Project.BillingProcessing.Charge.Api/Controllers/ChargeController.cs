using Microsoft.AspNetCore.Mvc;
using Project.BillingProcessing.Charge.Api.Application.Interface;
using Project.BillingProcessing.Charge.Api.Models;
using System.Net;

namespace Project.BillingProcessing.Charge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChargeController : ControllerBase
    {

        private readonly IChargeAppService _chargeAppService;

        public ChargeController(IChargeAppService chargeAppService)
        {
            _chargeAppService = chargeAppService;
        }

        [Route("GetChargeByParameter")]
        [HttpGet]
        [ProducesResponseType(typeof(Domain.ChargeEntity.Charge), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetChargeByParameter(string identification, DateTime? dueDate)
        {
            try
            {
                var charges = _chargeAppService.GetByParameter(identification, dueDate);
                return Ok(charges);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Route("createCharge")]
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodeResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> CreateCharge([FromBody] Domain.ChargeEntity.Charge chargeRequest)
        {
            try
            {
                var charge = _chargeAppService.InsertOneAsync(chargeRequest);
                return Ok(charge);
            }
            catch (Exception ex)
            {               
                return BadRequest(ex.Message);
            }
        }
    }
}
