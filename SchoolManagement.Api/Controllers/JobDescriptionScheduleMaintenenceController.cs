using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.JobDescriptionScheduleMaintenence)]
[ApiController]
[Authorize]
public class JobDescriptionScheduleMaintenenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobDescriptionScheduleMaintenenceController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-jobDescriptionScheduleMaintenences")] 
    public async Task<ActionResult<List<JobDescriptionScheduleMaintenenceDto>>> Get([FromQuery] QueryParams queryParams, int unitId, int departmentNameId, int itemDetailId)
    {
        var JobDescriptionScheduleMaintenences = await _mediator.Send(new GetJobDescriptionScheduleMaintenenceListRequest
        { 
            QueryParams = queryParams,
            UnitId =unitId,
            DepartmentNameId =departmentNameId,
            ItemDetailId =itemDetailId
        });
        return Ok(JobDescriptionScheduleMaintenences);
    }


    [HttpGet]
    [Route("get-jobDescriptionScheduleMaintenencesForMaintanence")] 
    public async Task<ActionResult<List<JobDescriptionScheduleMaintenenceDto>>> GetJobDescListForMaintanence([FromQuery] QueryParams queryParams, int unitId, int departmentNameId, int issueRegisterId, int scheduleMaintenenceTypeId)
    {
        var JobDescriptionScheduleMaintenences = await _mediator.Send(new GetJobDescriptionScheduleMaintenenceListForMaintanenceRequest
        { 
            QueryParams = queryParams,
            UnitId =unitId,
            DepartmentNameId =departmentNameId,
            IssueRegisterId = issueRegisterId,
            ScheduleMaintenenceTypeId =scheduleMaintenenceTypeId
        });
        return Ok(JobDescriptionScheduleMaintenences);
    }


    [HttpGet]
    [Route("get-jobDescriptionScheduleMaintenenceDetail/{id}")]
    public async Task<ActionResult<JobDescriptionScheduleMaintenenceDto>> Get(int id)
    {
        var JobDescriptionScheduleMaintenence = await _mediator.Send(new GetJobDescriptionScheduleMaintenenceDetailRequest { JobDescriptionScheduleMaintenenceId = id });
        return Ok(JobDescriptionScheduleMaintenence);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-jobDescriptionScheduleMaintenence")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobDescriptionScheduleMaintenenceDto JobDescriptionScheduleMaintenence)
    {
        var command = new CreateJobDescriptionScheduleMaintenenceCommand { JobDescriptionScheduleMaintenenceDto = JobDescriptionScheduleMaintenence };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-jobDescriptionScheduleMaintenence/{id}")]
    public async Task<ActionResult> Put([FromBody] JobDescriptionScheduleMaintenenceDto JobDescriptionScheduleMaintenence)
    {
        var command = new UpdateJobDescriptionScheduleMaintenenceCommand { JobDescriptionScheduleMaintenenceDto = JobDescriptionScheduleMaintenence };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-jobDescriptionScheduleMaintenence/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteJobDescriptionScheduleMaintenenceCommand { JobDescriptionScheduleMaintenenceId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedJobDescriptionScheduleMaintenence")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobDescriptionScheduleMaintenence()
    {
        var JobDescriptionScheduleMaintenence = await _mediator.Send(new GetSelectedJobDescriptionScheduleMaintenenceRequest { });
        return Ok(JobDescriptionScheduleMaintenence);
    }

    [HttpGet]
    [Route("get-selectedJobDescriptionForScheduleByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobDescriptionForScheduleByParameterRequest(int unitId,int departmentameId,int issueRegisterId,int scheduleMaintenenceTypeId,int fiscalYearId)
    {
        var jobDescription = await _mediator.Send(new GetSelectedJobDescriptionForScheduleByParameterRequest
        {
          UnitId =unitId,
          DepartmentNameId =departmentameId,
          IssueRegisterId =issueRegisterId,
          ScheduleMaintenenceTypeId =scheduleMaintenenceTypeId,
          FiscalYearId =fiscalYearId
        });
        return Ok(jobDescription);
    }
}

