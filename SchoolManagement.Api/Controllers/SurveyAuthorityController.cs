using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SurveyAuthority;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SurveyAuthority)]
[ApiController]
[Authorize]
public class SurveyAuthorityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveyAuthorityController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-SurveyAuthorities")]
    public async Task<ActionResult<List<SurveyAuthorityDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SurveyAuthoritys = await _mediator.Send(new GetSurveyAuthorityListRequest { QueryParams = queryParams });
        return Ok(SurveyAuthoritys);
    }


    [HttpGet]
    [Route("get-SurveyAuthorityDetail/{id}")]
    public async Task<ActionResult<SurveyAuthorityDto>> Get(int id)
    {
        var SurveyAuthority = await _mediator.Send(new GetSurveyAuthorityDetailRequest { SurveyAuthorityId = id });
        return Ok(SurveyAuthority);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-SurveyAuthority")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSurveyAuthorityDto SurveyAuthority)
    {
        var command = new CreateSurveyAuthorityCommand { SurveyAuthorityDto = SurveyAuthority };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-SurveyAuthority/{id}")]
    public async Task<ActionResult> Put([FromBody] SurveyAuthorityDto SurveyAuthority)
    {
        var command = new UpdateSurveyAuthorityCommand { SurveyAuthorityDto = SurveyAuthority };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-SurveyAuthority/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSurveyAuthorityCommand { SurveyAuthorityId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSurveyAuthoritys")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSurveyAuthority()
    {
        var SurveyAuthority = await _mediator.Send(new GetSelectedSurveyAuthorityRequest { });
        return Ok(SurveyAuthority);
    }
}

