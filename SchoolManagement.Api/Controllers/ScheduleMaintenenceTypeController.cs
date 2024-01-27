using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ScheduleMaintenenceType)]
[ApiController]
[Authorize]
public class ScheduleMaintenenceTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScheduleMaintenenceTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-scheduleMaintenenceTypes")]
    public async Task<ActionResult<List<ScheduleMaintenenceTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ScheduleMaintenenceTypes = await _mediator.Send(new GetScheduleMaintenenceTypeListRequest { QueryParams = queryParams });
        return Ok(ScheduleMaintenenceTypes);
    }


    [HttpGet]
    [Route("get-scheduleMaintenenceTypeDetail/{id}")]
    public async Task<ActionResult<ScheduleMaintenenceTypeDto>> Get(int id)
    {
        var ScheduleMaintenenceType = await _mediator.Send(new GetScheduleMaintenenceTypeDetailRequest { ScheduleMaintenenceTypeId = id });
        return Ok(ScheduleMaintenenceType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-scheduleMaintenenceType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateScheduleMaintenenceTypeDto ScheduleMaintenenceType)
    {
        var command = new CreateScheduleMaintenenceTypeCommand { ScheduleMaintenenceTypeDto = ScheduleMaintenenceType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-scheduleMaintenenceType/{id}")]
    public async Task<ActionResult> Put([FromBody] ScheduleMaintenenceTypeDto ScheduleMaintenenceType)
    {
        var command = new UpdateScheduleMaintenenceTypeCommand { ScheduleMaintenenceTypeDto = ScheduleMaintenenceType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-scheduleMaintenenceType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteScheduleMaintenenceTypeCommand { ScheduleMaintenenceTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedScheduleMaintenenceType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedScheduleMaintenenceType()
    {
        var ScheduleMaintenenceType = await _mediator.Send(new GetSelectedScheduleMaintenenceTypeRequest { });
        return Ok(ScheduleMaintenenceType);
    }
}

