using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.EquipmentPriority)]
[ApiController]
[Authorize]
public class EquipmentPriorityController : ControllerBase
{
    private readonly IMediator _mediator;

    public EquipmentPriorityController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-equipmentPrioritys")]
    public async Task<ActionResult<List<EquipmentPriorityDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var EquipmentPrioritys = await _mediator.Send(new GetEquipmentPriorityListRequest { QueryParams = queryParams });
        return Ok(EquipmentPrioritys);
    }


    [HttpGet]
    [Route("get-equipmentPriorityDetail/{id}")]
    public async Task<ActionResult<EquipmentPriorityDto>> Get(int id)
    {
        var EquipmentPriority = await _mediator.Send(new GetEquipmentPriorityDetailRequest { EquipmentPriorityId = id });
        return Ok(EquipmentPriority);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-equipmentPriority")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEquipmentPriorityDto EquipmentPriority)
    {
        var command = new CreateEquipmentPriorityCommand { EquipmentPriorityDto = EquipmentPriority };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-equipmentPriority/{id}")]
    public async Task<ActionResult> Put([FromBody] EquipmentPriorityDto EquipmentPriority)
    {
        var command = new UpdateEquipmentPriorityCommand { EquipmentPriorityDto = EquipmentPriority };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-equipmentPriority/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteEquipmentPriorityCommand { EquipmentPriorityId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedEquipmentPriority")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEquipmentPriority()
    {
        var EquipmentPriority = await _mediator.Send(new GetSelectedEquipmentPriorityRequest { });
        return Ok(EquipmentPriority);
    }
}

