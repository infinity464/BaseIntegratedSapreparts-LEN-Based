using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.MeaSquadronState;
using SchoolManagement.Application.Features.MeaSquadronStates.Requests.Commands;
using SchoolManagement.Application.Features.MeaSquadronStates.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.MeaSquadronState)]
[ApiController]
[Authorize]
public class MeaSquadronStateController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeaSquadronStateController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-MeaSquadronStates")]
    public async Task<ActionResult<List<MeaSquadronStateDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var MeaSquadronStates = await _mediator.Send(new GetMeaSquadronStateListRequest { QueryParams = queryParams });
        return Ok(MeaSquadronStates);
    }


    [HttpGet]
    [Route("get-MeaSquadronStateDetail/{id}")]
    public async Task<ActionResult<MeaSquadronStateDto>> Get(int id)
    {
        var MeaSquadronState = await _mediator.Send(new GetMeaSquadronStateDetailRequest { MeaSquadronStateId = id });
        return Ok(MeaSquadronState);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-MeaSquadronState")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateMeaSquadronStateDto MeaSquadronState)
    {
        var command = new CreateMeaSquadronStateCommand { MeaSquadronStateDto = MeaSquadronState };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-MeaSquadronState/{id}")]
    public async Task<ActionResult> Put([FromBody] MeaSquadronStateDto MeaSquadronState)
    {
        var command = new UpdateMeaSquadronStateCommand { MeaSquadronStateDto = MeaSquadronState };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-MeaSquadronState/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteMeaSquadronStateCommand { MeaSquadronStateId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    
}

