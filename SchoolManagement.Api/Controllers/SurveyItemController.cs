using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SurveyItem;
using SchoolManagement.Application.Features.SurveyItems.Requests.Commands;
using SchoolManagement.Application.Features.SurveyItems.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SurveyItem)]
[ApiController]
[Authorize]
public class SurveyItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveyItemController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-surveyItems")]
    public async Task<ActionResult<List<SurveyItemDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SurveyItems = await _mediator.Send(new GetSurveyItemListRequest { QueryParams = queryParams });
        return Ok(SurveyItems);
    }


    [HttpGet]
    [Route("get-surveyItemDetail/{id}")]
    public async Task<ActionResult<SurveyItemDto>> Get(int id)
    {
        var SurveyItem = await _mediator.Send(new GetSurveyItemDetailRequest { SurveyItemId = id });
        return Ok(SurveyItem);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-surveyItem")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSurveyItemDto SurveyItem)
    {
        var command = new CreateSurveyItemCommand { SurveyItemDto = SurveyItem };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-surveyItem/{id}")]
    public async Task<ActionResult> Put([FromBody] SurveyItemDto SurveyItem)
    {
        var command = new UpdateSurveyItemCommand { SurveyItemDto = SurveyItem };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-surveyItem/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSurveyItemCommand { SurveyItemId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSurveyItem")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSurveyItem()
    {
        var SurveyItem = await _mediator.Send(new GetSelectedSurveyItemRequest { });
        return Ok(SurveyItem);
    }
}

