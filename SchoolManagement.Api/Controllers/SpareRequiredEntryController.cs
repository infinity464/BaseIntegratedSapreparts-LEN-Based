using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SpareRequiredEntry)]
[ApiController]
[Authorize]
public class SpareRequiredEntryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpareRequiredEntryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-SpareRequiredEntries")]
    public async Task<ActionResult<List<SpareRequiredEntryDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SpareRequiredEntrys = await _mediator.Send(new GetSpareRequiredEntryListRequest { QueryParams = queryParams });
        return Ok(SpareRequiredEntrys);
    }


    [HttpGet]
    [Route("get-SpareRequiredEntryDetail/{id}")]
    public async Task<ActionResult<SpareRequiredEntryDto>> Get(int id)
    {
        var SpareRequiredEntry = await _mediator.Send(new GetSpareRequiredEntryDetailRequest { SpareRequiredEntryId = id });
        return Ok(SpareRequiredEntry);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-SpareRequiredEntry")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSpareRequiredEntryDto SpareRequiredEntry)
    {
        var command = new CreateSpareRequiredEntryCommand { SpareRequiredEntryDto = SpareRequiredEntry };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-SpareRequiredEntry/{id}")]
    public async Task<ActionResult> Put([FromBody] SpareRequiredEntryDto SpareRequiredEntry)
    {
        var command = new UpdateSpareRequiredEntryCommand { SpareRequiredEntryDto = SpareRequiredEntry };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-SpareRequiredEntry/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSpareRequiredEntryCommand { SpareRequiredEntryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSpareRequiredEntries")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSpareRequiredEntry()
    {
        var SpareRequiredEntry = await _mediator.Send(new GetSelectedSpareRequiredEntryRequest { });
        return Ok(SpareRequiredEntry);
    }
    [HttpGet]
    [Route("get-spareRequiredEntryByDepartment")]
    public async Task<ActionResult> GetSpareRequiredEntryByDepartment(int departmentNameId)
    {
        var pmsNumberEntry = await _mediator.Send(new GetSpareRequiredEntryByDepartmentRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(pmsNumberEntry);
    }

    [HttpGet]
    [Route("get-spareRequiredEntryByJobDesc")]
    public async Task<ActionResult> GetSpareRequiredEntryByJobDesc(int jobDescPmsId)
    {
        var pmsNumberEntry = await _mediator.Send(new GetSpareRequiredEntryByJobDescRequest
        {
            JobDescriptionEntryPmsId = jobDescPmsId
        });
        return Ok(pmsNumberEntry);
    }
}

