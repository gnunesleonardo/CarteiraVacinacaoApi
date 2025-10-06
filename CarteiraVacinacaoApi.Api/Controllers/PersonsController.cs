using CarteiraVacinacaoApi.Application.Commands.CreatePerson;
using CarteiraVacinacaoApi.Application.Commands.DeletePerson;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraVacinacaoApi.Api.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreatePerson), result);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            var command = new DeletePersonCommand(id);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
