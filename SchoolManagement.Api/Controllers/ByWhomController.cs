using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ByWhom;
using SchoolManagement.Application.Features.ByWhoms.Requests.Commands;
using SchoolManagement.Application.Features.ByWhoms.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ByWhom)]
[ApiController]
[Authorize]
public class ByWhomController : ControllerBase
{
    private readonly IMediator _mediator;

    public ByWhomController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ByWhoms")]
    public async Task<ActionResult<List<ByWhomDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ByWhoms = await _mediator.Send(new GetByWhomListRequest { QueryParams = queryParams });
        return Ok(ByWhoms);
    }


    [HttpGet]
    [Route("get-ByWhomDetail/{id}")]
    public async Task<ActionResult<ByWhomDto>> Get(int id)
    {
        var ByWhom = await _mediator.Send(new GetByWhomDetailRequest { ByWhomId = id });
        return Ok(ByWhom);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ByWhom")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateByWhomDto ByWhom)
    {
        var command = new CreateByWhomCommand { ByWhomDto = ByWhom };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ByWhom/{id}")]
    public async Task<ActionResult> Put([FromBody] ByWhomDto ByWhom)
    {
        var command = new UpdateByWhomCommand { ByWhomDto = ByWhom };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ByWhom/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteByWhomCommand { ByWhomId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedByWhoms")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedByWhom()
    {
        var ByWhom = await _mediator.Send(new GetSelectedByWhomRequest { });
        return Ok(ByWhom);
    }
}

