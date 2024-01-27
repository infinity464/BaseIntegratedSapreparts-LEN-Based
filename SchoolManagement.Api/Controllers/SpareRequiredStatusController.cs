using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Commands;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SpareRequiredStatus)]
[ApiController]
[Authorize]
public class SpareRequiredStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpareRequiredStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-spareRequiredStatuss")]
    public async Task<ActionResult<List<SpareRequiredStatusDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SpareRequiredStatuss = await _mediator.Send(new GetSpareRequiredStatusListRequest { QueryParams = queryParams });
        return Ok(SpareRequiredStatuss);
    }


    [HttpGet]
    [Route("get-spareRequiredStatusDetail/{id}")]
    public async Task<ActionResult<SpareRequiredStatusDto>> Get(int id)
    {
        var SpareRequiredStatus = await _mediator.Send(new GetSpareRequiredStatusDetailRequest { SpareRequiredStatusId = id });
        return Ok(SpareRequiredStatus);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-spareRequiredStatus")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSpareRequiredStatusDto SpareRequiredStatus)
    {
        var command = new CreateSpareRequiredStatusCommand { SpareRequiredStatusDto = SpareRequiredStatus };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-spareRequiredStatus/{id}")]
    public async Task<ActionResult> Put([FromBody] SpareRequiredStatusDto SpareRequiredStatus)
    {
        var command = new UpdateSpareRequiredStatusCommand { SpareRequiredStatusDto = SpareRequiredStatus };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-spareRequiredStatus/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSpareRequiredStatusCommand { SpareRequiredStatusId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSpareRequiredStatus")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSpareRequiredStatus()
    {
        var SpareRequiredStatus = await _mediator.Send(new GetSelectedSpareRequiredStatusRequest { });
        return Ok(SpareRequiredStatus);
    }
}

