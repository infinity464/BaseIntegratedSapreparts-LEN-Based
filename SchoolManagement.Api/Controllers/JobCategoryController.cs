using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.JobCategory;
using SchoolManagement.Application.Features.JobCategorys.Requests.Commands;
using SchoolManagement.Application.Features.JobCategorys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.JobCategory)]
[ApiController]
[Authorize]
public class JobCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-jobCategories")]
    public async Task<ActionResult<List<JobCategoryDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var JobCategorys = await _mediator.Send(new GetJobCategoryListRequest { QueryParams = queryParams });
        return Ok(JobCategorys);
    }


    [HttpGet]
    [Route("get-JobCategoryDetail/{id}")]
    public async Task<ActionResult<JobCategoryDto>> Get(int id)
    {
        var JobCategory = await _mediator.Send(new GetJobCategoryDetailRequest { JobCategoryId = id });
        return Ok(JobCategory);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-JobCategory")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobCategoryDto JobCategory)
    {
        var command = new CreateJobCategoryCommand { JobCategoryDto = JobCategory };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-JobCategory/{id}")]
    public async Task<ActionResult> Put([FromBody] JobCategoryDto JobCategory)
    {
        var command = new UpdateJobCategoryCommand { JobCategoryDto = JobCategory };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-JobCategory/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteJobCategoryCommand { JobCategoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPriorities")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobCategory()
    {
        var JobCategory = await _mediator.Send(new GetSelectedJobCategoryRequest { });
        return Ok(JobCategory);
    }
}

