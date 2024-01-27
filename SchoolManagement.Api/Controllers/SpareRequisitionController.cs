using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SpareRequisition;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SpareRequisition)]
[ApiController]
[Authorize]
public class SpareRequisitionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpareRequisitionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-spareRequisitions")]
    public async Task<ActionResult<List<SpareRequisitionDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SpareRequisitions = await _mediator.Send(new GetSpareRequisitionListRequest { QueryParams = queryParams });
        return Ok(SpareRequisitions);
    }


    [HttpGet]
    [Route("get-spareRequisitionDetail/{id}")]
    public async Task<ActionResult<SpareRequisitionDto>> Get(int id)
    {
        var SpareRequisition = await _mediator.Send(new GetSpareRequisitionDetailRequest { SpareRequisitionId = id });
        return Ok(SpareRequisition);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-spareRequisition")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSpareRequisitionDto SpareRequisition)
    {
        var command = new CreateSpareRequisitionCommand { SpareRequisitionDto = SpareRequisition };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-spareRequisition/{id}")]
    public async Task<ActionResult> Put([FromBody] SpareRequisitionDto SpareRequisition)
    {
        var command = new UpdateSpareRequisitionCommand { SpareRequisitionDto = SpareRequisition };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-spareRequisition/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSpareRequisitionCommand { SpareRequisitionId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSpareRequisition")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSpareRequisition()
    {
        var SpareRequisition = await _mediator.Send(new GetSelectedSpareRequisitionRequest { });
        return Ok(SpareRequisition);
    }
}

