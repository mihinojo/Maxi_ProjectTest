using MaxiEmployeeAPI.IServices;
using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private IBeneficiaryService _beneficiaryService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService)
        {
            _beneficiaryService = beneficiaryService;
        }

        [HttpGet("{idEmployee:int}")]
        public async Task<ActionResult<Response>> Get(int idEmployee)
        {
            return new Response { IsSuccess = true, Result = await _beneficiaryService.GetBeneficiariesByEmployeeId(idEmployee) };
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] Beneficiary beneficiary)
        {
            var emp = await _beneficiaryService.Add(beneficiary);
            return emp != null ? new Response { IsSuccess = true, Message = "Beneficiary saved successfully", Result = emp } : new Response { IsSuccess = false, Message = "Error trying to add Beneficiary", Result = emp };
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put([FromBody] Beneficiary beneficiary)
        {
            var emp = await _beneficiaryService.Update(beneficiary);
            return emp != null ? new Response { IsSuccess = true, Message = "Beneficiary updated successfully", Result = beneficiary } : new Response { IsSuccess = false, Message = "Error trying to update Beneficiary", Result = beneficiary };
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var message = await _beneficiaryService.Delete(id);
            return string.IsNullOrEmpty(message) ? new Response { IsSuccess = true, Message = "Beneficiary deleted successfully", Result = id } : new Response { IsSuccess = false, Message = "Error trying to delete Beneficiary", Result = id };
        }

    }
}
