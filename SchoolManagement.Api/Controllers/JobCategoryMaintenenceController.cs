using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.JobCategoryMaintenence)]
[ApiController]
[Authorize] 
public class JobCategoryMaintenenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobCategoryMaintenenceController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-jobCategoryMaintenences")]
    public async Task<ActionResult<List<JobCategoryMaintenenceDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var JobCategoryMaintenences = await _mediator.Send(new GetJobCategoryMaintenenceListRequest { QueryParams = queryParams });
        return Ok(JobCategoryMaintenences);
    }


    [HttpGet]
    [Route("get-jobCategoryMaintenenceDetail/{id}")]
    public async Task<ActionResult<JobCategoryMaintenenceDto>> Get(int id)
    {
        var JobCategoryMaintenence = await _mediator.Send(new GetJobCategoryMaintenenceDetailRequest { JobCategoryMaintenenceId = id });
        return Ok(JobCategoryMaintenence);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-jobCategoryMaintenence")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobCategoryMaintenenceDto JobCategoryMaintenence)
    {
        var command = new CreateJobCategoryMaintenenceCommand { JobCategoryMaintenenceDto = JobCategoryMaintenence };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-jobCategoryMaintenence/{id}")]
    public async Task<ActionResult> Put([FromBody] JobCategoryMaintenenceDto JobCategoryMaintenence)
    {
        var command = new UpdateJobCategoryMaintenenceCommand { JobCategoryMaintenenceDto = JobCategoryMaintenence };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-jobCategoryMaintenence/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteJobCategoryMaintenenceCommand { JobCategoryMaintenenceId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedJobCategoryMaintenence")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobCategoryMaintenence()
    {
        var JobCategoryMaintenence = await _mediator.Send(new GetSelectedJobCategoryMaintenenceRequest { });
        return Ok(JobCategoryMaintenence);
    }
}

