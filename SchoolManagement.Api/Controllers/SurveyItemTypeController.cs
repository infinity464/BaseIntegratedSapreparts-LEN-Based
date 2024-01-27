using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SurveyItemType;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SurveyItemType)]
[ApiController]
[Authorize]
public class SurveyItemTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveyItemTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-SurveyItemTypes")]
    public async Task<ActionResult<List<SurveyItemTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SurveyItemTypes = await _mediator.Send(new GetSurveyItemTypeListRequest { QueryParams = queryParams });
        return Ok(SurveyItemTypes);
    }


    [HttpGet]
    [Route("get-SurveyItemTypeDetail/{id}")]
    public async Task<ActionResult<SurveyItemTypeDto>> Get(int id)
    {
        var SurveyItemType = await _mediator.Send(new GetSurveyItemTypeDetailRequest { SurveyItemTypeId = id });
        return Ok(SurveyItemType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-SurveyItemType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSurveyItemTypeDto SurveyItemType)
    {
        var command = new CreateSurveyItemTypeCommand { SurveyItemTypeDto = SurveyItemType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-SurveyItemType/{id}")]
    public async Task<ActionResult> Put([FromBody] SurveyItemTypeDto SurveyItemType)
    {
        var command = new UpdateSurveyItemTypeCommand { SurveyItemTypeDto = SurveyItemType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-SurveyItemType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSurveyItemTypeCommand { SurveyItemTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSurveyItemTypes")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSurveyItemType()
    {
        var SurveyItemType = await _mediator.Send(new GetSelectedSurveyItemTypeRequest { });
        return Ok(SurveyItemType);
    }
}

