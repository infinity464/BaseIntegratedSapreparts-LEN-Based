using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.IssueType;
using SchoolManagement.Application.Features.IssueTypes.Requests.Commands;
using SchoolManagement.Application.Features.IssueTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.IssueType)]
[ApiController]
[Authorize]
public class IssueTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-IssueTypes")]
    public async Task<ActionResult<List<IssueTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var IssueTypes = await _mediator.Send(new GetIssueTypeListRequest { QueryParams = queryParams });
        return Ok(IssueTypes);
    }


    [HttpGet]
    [Route("get-IssueTypeDetail/{id}")]
    public async Task<ActionResult<IssueTypeDto>> Get(int id)
    {
        var IssueType = await _mediator.Send(new GetIssueTypeDetailRequest { IssueTypeId = id });
        return Ok(IssueType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-IssueType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateIssueTypeDto IssueType)
    {
        var command = new CreateIssueTypeCommand { IssueTypeDto = IssueType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-IssueType/{id}")]
    public async Task<ActionResult> Put([FromBody] IssueTypeDto IssueType)
    {
        var command = new UpdateIssueTypeCommand { IssueTypeDto = IssueType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-IssueType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteIssueTypeCommand { IssueTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedIssueTypes")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedIssueType()
    {
        var IssueType = await _mediator.Send(new GetSelectedIssueTypeRequest { });
        return Ok(IssueType);
    }
}

