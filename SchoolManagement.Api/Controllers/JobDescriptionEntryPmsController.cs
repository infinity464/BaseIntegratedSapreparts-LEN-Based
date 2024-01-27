using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.JobDescriptionEntryPms)]
[ApiController]
[Authorize]
public class JobDescriptionEntryPmsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobDescriptionEntryPmsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-JobDescriptionEntryPmses")]
    public async Task<ActionResult<List<JobDescriptionEntryPmsDto>>> Get([FromQuery] QueryParams queryParams, int unitId, int departmentNameId)
    {
        var JobDescriptionEntryPms = await _mediator.Send(new GetJobDescriptionEntryPmsListRequest { 
            QueryParams = queryParams,
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(JobDescriptionEntryPms);
    }


    [HttpGet]
    [Route("get-jobDescriptionEntryByUnitAndDeptandPmsNumberId")]
    public async Task<ActionResult<List<JobDescriptionEntryPmsDto>>> GetJobDescriptionEntryByUnitAndDept([FromQuery] QueryParams queryParams, int unitId, int departmentNameId, int pmsNumberEntryId)
    {
        var JobDescriptionEntryPms = await _mediator.Send(new GetJobDescriptionEntryPmsListByDeptIdUnitAndPmsNumbeIdRequest
        {
            QueryParams = queryParams,
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            PmsNumberEntryId =pmsNumberEntryId
        });
        return Ok(JobDescriptionEntryPms);
    }

    [HttpGet]
    [Route("get-JobDescriptionEntryPmsDetail/{id}")]
    public async Task<ActionResult<JobDescriptionEntryPmsDto>> Get(int id)
    {
        var JobDescriptionEntryPms = await _mediator.Send(new GetJobDescriptionEntryPmsDetailRequest { JobDescriptionEntryPmsId = id });
        return Ok(JobDescriptionEntryPms);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-JobDescriptionEntryPms")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobDescriptionEntryPmsDto JobDescriptionEntryPms)
    {
        var command = new CreateJobDescriptionEntryPmsCommand { JobDescriptionEntryPmsDto = JobDescriptionEntryPms };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-JobDescriptionEntryPms/{id}")]
    public async Task<ActionResult> Put([FromBody] JobDescriptionEntryPmsDto JobDescriptionEntryPms)
    {
        var command = new UpdateJobDescriptionEntryPmsCommand { JobDescriptionEntryPmsDto = JobDescriptionEntryPms };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-JobDescriptionEntryPms/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteJobDescriptionEntryPmsCommand { JobDescriptionEntryPmsId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedJobDescriptionEntryPmses")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobDescriptionEntryPms()
    {
        var JobDescriptionEntryPms = await _mediator.Send(new GetSelectedJobDescriptionEntryPmsRequest { });
        return Ok(JobDescriptionEntryPms);
    }
    [HttpGet]
    [Route("get-jobDescriptionEntryPmsByDepartment")]
    public async Task<ActionResult> GetJobDescriptionEntryPmsByDepartment(int departmentNameId)
    {
        var pmsNumberEntry = await _mediator.Send(new GetJobDescriptionEntryPmsByDepartmentRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(pmsNumberEntry);
    }
    [HttpGet]
    [Route("get-selectedJICByJobDescriptionEntryPmsId")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJICByJobDescriptionEntryPmsId(int jobDescriptionEntryPmsId)
    {
        var msNoByPmsNumberEntryId = await _mediator.Send(new GetJICByJobDescriptionEntryPmsIdRequest { JobDescriptionEntryPmsId = jobDescriptionEntryPmsId });
        return Ok(msNoByPmsNumberEntryId);
    }  

    [HttpGet]
    [Route("get-selectedJobDescriptionEntryPmsIdByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedJobDescriptionEntryPmsIdByParameterRequest(int unitId,int departmentNameId,int maintenenceTypeId,int maintenenceCategoryId,int itemDetailId)
    {
        var msNoByPmsNumberEntryId = await _mediator.Send(new GetJobDescriptionEntryPmsIdByParameterRequest
        { 
            UnitId = unitId,
            DepartmentNameId= departmentNameId,
            MaintenanceTypeId = maintenenceTypeId,
            MaintenanceCategoryId = maintenenceCategoryId,
            ItemDetailId = itemDetailId,
         });
        return Ok(msNoByPmsNumberEntryId);
    }
    [HttpGet]
    [Route("get-jicByUnitAndDepartmentAndItemDetailId")]
    public async Task<ActionResult<List<SelectedModel>>> GetJICByUnitAndDepartmentAndCategoryId(int unitId, int departmentNameId, int itemDetailId)
    {
        var jic = await _mediator.Send(new GetJICByUnitAndDepartmentAndItemDetailIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemDetailId = itemDetailId
        });
        return Ok(jic);
    }
    [HttpGet]
    [Route("get-JobDescriptionListByParameter")]
    public async Task<ActionResult<List<SelectedModel>>> JobDescriptionListByParameter(int unitId, int departmentNameId, int itemDetailId)
    {
        var equipment = await _mediator.Send(new GetJobDescriptionListByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemDetailId = itemDetailId
        });
        return Ok(equipment);
    }
     
    [HttpGet]
    [Route("get-jobDescriptionEntryPmsListForPreventive")]
    public async Task<ActionResult> GetJobDescriptionEntryPmsForPreventiveList(int unitId, int departmentNameId, int maintenenceTypeId, int maintenenceCategoryId, int itemDetailId)
    {
        var pmsNumberEntry = await _mediator.Send(new GetJobDescriptionEntryPmsListForPreventiveRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            MaintenanceTypeId = maintenenceTypeId,
            MaintenanceCategoryId = maintenenceCategoryId,
            ItemDetailId = itemDetailId,
        });
        return Ok(pmsNumberEntry);
    }
}

