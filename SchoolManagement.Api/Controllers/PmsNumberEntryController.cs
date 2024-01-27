using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.PmsNumberEntry)]
[ApiController]
[Authorize]
public class PmsNumberEntryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PmsNumberEntryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-PmsNumberEntries")]
    public async Task<ActionResult<List<PmsNumberEntryDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var PmsNumberEntrys = await _mediator.Send(new GetPmsNumberEntryListRequest { QueryParams = queryParams });
        return Ok(PmsNumberEntrys);
    }
     
    [HttpGet]
    [Route("get-PmsNumberEntriesByUnitIdDeptId")]
    public async Task<ActionResult<List<PmsNumberEntryDto>>> GetPmsNumberEntryByUnitIdAndDeptId([FromQuery] QueryParams queryParams,int unitId,int departmentNameId,int itemDetailId)
    {
        var PmsNumberEntrys = await _mediator.Send(new GetPmsNumberEntryListByUnitAndDeptIdRequest 
        { 
            QueryParams = queryParams,
            UnitId = unitId,
            DepartmentNameId =departmentNameId,
            ItemDetailId =itemDetailId
        });
        return Ok(PmsNumberEntrys);
    }


    [HttpGet]
    [Route("get-PmsNumberEntryDetail/{id}")]
    public async Task<ActionResult<PmsNumberEntryDto>> Get(int id)
    {
        var PmsNumberEntry = await _mediator.Send(new GetPmsNumberEntryDetailRequest { PmsNumberEntryId = id });
        return Ok(PmsNumberEntry);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-PmsNumberEntry")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePmsNumberEntryDto PmsNumberEntry)
    {
        var command = new CreatePmsNumberEntryCommand { PmsNumberEntryDto = PmsNumberEntry };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-PmsNumberEntry/{id}")]
    public async Task<ActionResult> Put([FromBody] PmsNumberEntryDto PmsNumberEntry)
    {
        var command = new UpdatePmsNumberEntryCommand { PmsNumberEntryDto = PmsNumberEntry };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-PmsNumberEntry/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePmsNumberEntryCommand { PmsNumberEntryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPmsNumberEntries")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPmsNumberEntry()
    {
        var PmsNumberEntry = await _mediator.Send(new GetSelectedPmsNumberEntryRequest { });
        return Ok(PmsNumberEntry);
    }
    [HttpGet]
    [Route("get-pmsNumberEntryByDepartment")]
    public async Task<ActionResult> GetPmsNumberEntryByDepartment(int departmentNameId)
    {
        var pmsNumberEntry = await _mediator.Send(new GetPmsNumberEntryByDepartmentRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(pmsNumberEntry);
    }
    [HttpGet]
    [Route("get-selectedItemNameFromPmsNumberEntry")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedItemNameFromPmsNumberEntry(int unitId, int departmentNameId)
    {
        var selectedPmsNumberEntry = await _mediator.Send(new GetSelectedItemNameFromPmsNumberEntryRequest { 
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(selectedPmsNumberEntry);
    }
    [HttpGet]
    [Route("get-selectedPmsNoByPmsNumberEntryId")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPmsNoByPmsNumberEntryId(int pmsNumberEntryId)
    {
        var msNoByPmsNumberEntryId = await _mediator.Send(new GetPmsNoByPmsNumberEntryIdRequest { PmsNumberEntryId = pmsNumberEntryId });
        return Ok(msNoByPmsNumberEntryId);
    }
}

