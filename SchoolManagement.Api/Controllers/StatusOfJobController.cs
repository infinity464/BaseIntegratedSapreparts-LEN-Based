using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.StatusOfJob;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.StatusOfJob)]
[ApiController]
[Authorize]
public class StatusOfJobController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatusOfJobController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-StatusOfJobs")]
    public async Task<ActionResult<List<StatusOfJobDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var StatusOfJobs = await _mediator.Send(new GetStatusOfJobListRequest { QueryParams = queryParams });
        return Ok(StatusOfJobs);
    }


    [HttpGet]
    [Route("get-StatusOfJobDetail/{id}")]
    public async Task<ActionResult<StatusOfJobDto>> Get(int id)
    {
        var StatusOfJob = await _mediator.Send(new GetStatusOfJobDetailRequest { StatusOfJobId = id });
        return Ok(StatusOfJob);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-StatusOfJob")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateStatusOfJobDto StatusOfJob)
    {
        var command = new CreateStatusOfJobCommand { StatusOfJobDto = StatusOfJob };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-StatusOfJob/{id}")]
    public async Task<ActionResult> Put([FromBody] StatusOfJobDto StatusOfJob)
    {
        var command = new UpdateStatusOfJobCommand { StatusOfJobDto = StatusOfJob };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-StatusOfJob/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteStatusOfJobCommand { StatusOfJobId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedStatusOfJobs")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedStatusOfJob()
    {
        var StatusOfJob = await _mediator.Send(new GetSelectedStatusOfJobRequest { });
        return Ok(StatusOfJob);
    }
}

