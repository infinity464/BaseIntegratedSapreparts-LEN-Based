using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.ReturnLogs;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Commands;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ReturnLog)]
[ApiController]
[Authorize]
public class ReturnLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReturnLogController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ReturnLogs")]
    public async Task<ActionResult<List<ReturnLogDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReturnLogs = await _mediator.Send(new GetReturnLogListRequest { QueryParams = queryParams });
        return Ok(ReturnLogs);
    }


    [HttpGet]
    [Route("get-ReturnLogDetail/{id}")]
    public async Task<ActionResult<ReturnLogDto>> Get(int id)
    {
        var ReturnLog = await _mediator.Send(new GetReturnLogDetailRequest { ReturnLogId = id });
        return Ok(ReturnLog);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ReturnLog")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] AddReturnLogToStoreDto ReturnLog)
    {
        var command = new AddingReturnLogRegisterCommand { IssueRegisterDtos = ReturnLog };
        var response = await _mediator.Send(command);
        return Ok(response); 
    }
     

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ReturnLog/{id}")]
    public async Task<ActionResult> Put([FromBody] ReturnLogDto ReturnLog)
    {
        var command = new UpdateReturnLogCommand { ReturnLogDto = ReturnLog };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ReturnLog/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReturnLogCommand { ReturnLogId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedReturnLog")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReturnLog()
    {
        var ReturnLog = await _mediator.Send(new GetSelectedReturnLogRequest { });
        return Ok(ReturnLog);
    }
}

