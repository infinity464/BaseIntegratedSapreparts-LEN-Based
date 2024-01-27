using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Progres;
using SchoolManagement.Application.Features.Progress.Requests.Commands;
using SchoolManagement.Application.Features.Progress.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Progres)]
[ApiController]
[Authorize]
public class ProgresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProgresController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Priorities")]
    public async Task<ActionResult<List<ProgresDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Progress = await _mediator.Send(new GetProgresListRequest { QueryParams = queryParams });
        return Ok(Progress);
    }


    [HttpGet]
    [Route("get-ProgresDetail/{id}")]
    public async Task<ActionResult<ProgresDto>> Get(int id)
    {
        var Progres = await _mediator.Send(new GetProgresDetailRequest { ProgresId = id });
        return Ok(Progres);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Progres")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProgresDto Progres)
    {
        var command = new CreateProgresCommand { ProgresDto = Progres };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Progres/{id}")]
    public async Task<ActionResult> Put([FromBody] ProgresDto Progres)
    {
        var command = new UpdateProgresCommand { ProgresDto = Progres };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Progres/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProgresCommand { ProgresId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPriorities")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedProgres()
    {
        var Progres = await _mediator.Send(new GetSelectedProgresRequest { });
        return Ok(Progres);
    }
}

