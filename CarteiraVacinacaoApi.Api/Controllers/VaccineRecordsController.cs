using CarteiraVacinacaoApi.Application.Commands.CreateVaccineRecord;
using CarteiraVacinacaoApi.Application.Commands.DeleteVaccineRecord;
using CarteiraVacinacaoApi.Application.Queries.GetVaccinationCard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraVacinacaoApi.Api.Controllers
{
    [ApiController]
    [Route("api/vaccine-records")]
    public class VaccineRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VaccineRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVaccineRecord([FromBody] CreateVaccineRecordCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateVaccineRecord), result);
        }

        [HttpGet]
        [Authorize]
        [Route("card/{personId}")]
        public async Task<IActionResult> GetVaccinationCard([FromRoute] int personId)
        {
            var command = new GetVaccinationCardQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVaccineRecord([FromRoute] int id)
        {
            var command = new DeleteVaccineRecordCommand(id);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
