using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.JobDescription;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Commands;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.JobDescription)]
[ApiController]
[Authorize]
public class JobDescriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobDescriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-JobDescriptions")]
    public async Task<ActionResult<List<JobDescriptionDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var JobDescriptions = await _mediator.Send(new GetJobDescriptionListRequest { QueryParams = queryParams });
        return Ok(JobDescriptions);
    }


    [HttpGet]
    [Route("get-JobDescriptionDetail/{id}")]
    public async Task<ActionResult<JobDescriptionDto>> Get(int id)
    {
        var JobDescription = await _mediator.Send(new GetJobDescriptionDetailRequest { JobDescriptionId = id });
        return Ok(JobDescription);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-JobDescription")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobDescriptionDto JobDescription)
    {
        var command = new CreateJobDescriptionCommand { JobDescriptionDto = JobDescription };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-JobDescription/{id}")]
    public async Task<ActionResult> Put([FromBody] JobDescriptionDto JobDescription)
    {
        var command = new UpdateJobDescriptionCommand { JobDescriptionDto = JobDescription };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-JobDescription/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteJobDescriptionCommand { JobDescriptionId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPriorities")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobDescription()
    {
        var JobDescription = await _mediator.Send(new GetSelectedJobDescriptionRequest { });
        return Ok(JobDescription);
    }
}

