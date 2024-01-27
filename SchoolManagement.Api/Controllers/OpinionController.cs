using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Opinion;
using SchoolManagement.Application.Features.Opinions.Requests.Commands;
using SchoolManagement.Application.Features.Opinions.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Opinion)]
[ApiController]
[Authorize]
public class OpinionController : ControllerBase
{
    private readonly IMediator _mediator;

    public OpinionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Opinions")]
    public async Task<ActionResult<List<OpinionDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Opinions = await _mediator.Send(new GetOpinionListRequest { QueryParams = queryParams });
        return Ok(Opinions);
    }


    [HttpGet]
    [Route("get-OpinionDetail/{id}")]
    public async Task<ActionResult<OpinionDto>> Get(int id)
    {
        var Opinion = await _mediator.Send(new GetOpinionDetailRequest { OpinionId = id });
        return Ok(Opinion);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Opinion")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOpinionDto Opinion)
    {
        var command = new CreateOpinionCommand { OpinionDto = Opinion };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Opinion/{id}")]
    public async Task<ActionResult> Put([FromBody] OpinionDto Opinion)
    {
        var command = new UpdateOpinionCommand { OpinionDto = Opinion };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Opinion/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteOpinionCommand { OpinionId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedOpinions")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedOpinion()
    {
        var Opinion = await _mediator.Send(new GetSelectedOpinionRequest { });
        return Ok(Opinion);
    }
}

