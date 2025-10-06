using CarteiraVacinacaoApi.Application.Commands.CreateVaccine;
using CarteiraVacinacaoApi.Application.Queries.GetAllVaccines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraVacinacaoApi.Api.Controllers
{
    [ApiController]
    [Route("api/vaccines")]
    public class VaccinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VaccinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVaccines()
        {
            var result = await _mediator.Send(new GetAllVaccineQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVaccine([FromBody]CreateVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateVaccine), result);
        }
    }
}
