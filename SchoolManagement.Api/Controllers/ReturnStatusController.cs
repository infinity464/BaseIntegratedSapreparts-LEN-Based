using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ReturnStatus;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ReturnStatus)]
[ApiController]
[Authorize]
public class ReturnStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReturnStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-returnStatuses")]
    public async Task<ActionResult<List<ReturnStatusDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReturnStatuss = await _mediator.Send(new GetReturnStatusListRequest { QueryParams = queryParams });
        return Ok(ReturnStatuss);
    }


    [HttpGet]
    [Route("get-returnStatusDetail/{id}")]
    public async Task<ActionResult<ReturnStatusDto>> Get(int id)
    {
        var ReturnStatus = await _mediator.Send(new GetReturnStatusDetailRequest { ReturnStatusId = id });
        return Ok(ReturnStatus);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-returnStatus")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReturnStatusDto ReturnStatus)
    {
        var command = new CreateReturnStatusCommand { ReturnStatusDto = ReturnStatus };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-returnStatus/{id}")]
    public async Task<ActionResult> Put([FromBody] ReturnStatusDto ReturnStatus)
    {
        var command = new UpdateReturnStatusCommand { ReturnStatusDto = ReturnStatus };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-returnStatus/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReturnStatusCommand { ReturnStatusId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedReturnStatus")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReturnStatus()
    {
        var ReturnStatus = await _mediator.Send(new GetSelectedReturnStatusRequest { });
        return Ok(ReturnStatus);
    }
}

