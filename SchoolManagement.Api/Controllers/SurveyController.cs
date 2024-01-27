using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Survey;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Application.Features.Surveys.Requests.Commands;
using SchoolManagement.Application.Features.Surveys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Survey)]
[ApiController]
[Authorize]
public class SurveyController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveyController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Surveys")]
    public async Task<ActionResult<List<SurveyDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Surveys = await _mediator.Send(new GetSurveyListRequest { QueryParams = queryParams });
        return Ok(Surveys);
    }


    [HttpGet]
    [Route("get-SurveyDetail/{id}")]
    public async Task<ActionResult<SurveyDto>> Get(int id)
    {
        var Survey = await _mediator.Send(new GetSurveyDetailRequest { SurveyId = id });
        return Ok(Survey);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Survey")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSurveyDto Survey)
    {
        var command = new CreateSurveyCommand { SurveyDto = Survey };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Survey/{id}")]
    public async Task<ActionResult> Put([FromBody] SurveyDto Survey)
    {
        var command = new UpdateSurveyCommand { SurveyDto = Survey };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Survey/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSurveyCommand { SurveyId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSurvey")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSurvey()
    {
        var Survey = await _mediator.Send(new GetSelectedSurveyRequest { });
        return Ok(Survey);
    }

    [HttpGet]
    [Route("get-selectedSurveyByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSurveyByParameterRequest(int unitId,int departmentNameId,int itemCategoryId)
    {
        var Survey = await _mediator.Send(new GetSelectSurveyByUnitAndDepartmentIdRequest
        {
            UnitId = unitId,
            DepartmentNameId =departmentNameId,
            ItemCategoryId =itemCategoryId,
           // SurveyAuthorityId =surveyAuthorityId
        });
        return Ok(Survey);
    }
}

