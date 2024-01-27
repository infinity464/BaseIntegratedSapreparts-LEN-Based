using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.MaintenanceSchedule;
using SchoolManagement.Application.Features.MaintenanceSchedules.Requests.Commands;
using SchoolManagement.Application.Features.MaintenanceSchedules.Requests.Queries;
using SchoolManagement.Shared.Models;


namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.MaintenanceSchedule)]
[ApiController]
[Authorize]
public class MaintenanceScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-MaintenanceSchedules")]
    public async Task<ActionResult<List<MaintenanceScheduleDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var MaintenanceSchedules = await _mediator.Send(new GetMaintenanceScheduleListRequest { QueryParams = queryParams });
        return Ok(MaintenanceSchedules);
    }

    [HttpGet]
    [Route("get-MaintenanceScheduleDetail/{id}")]
    public async Task<ActionResult<MaintenanceScheduleDto>> Get(int id)
    {
        var MaintenanceSchedule = await _mediator.Send(new GetMaintenanceScheduleDetailRequest { MaintenanceScheduleId = id });
        return Ok(MaintenanceSchedule);
    }



    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-MaintenanceSchedule")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateMaintenanceScheduleDto MaintenanceSchedule)
    {
        var command = new CreateMaintenanceScheduleCommand { MaintenanceScheduleDto = MaintenanceSchedule };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-MaintenanceSchedule/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateMaintenanceScheduleDto maintenanceSchedule)
    {
        var command = new UpdateMaintenanceScheduleCommand { UpdateMaintenanceScheduleDto = maintenanceSchedule };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-MaintenanceSchedule/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteMaintenanceScheduleCommand { MaintenanceScheduleId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedMaintenanceSchedules")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedMaintenanceSchedule()
    {
        var selectedMaintenanceSchedule = await _mediator.Send(new GetSelectedMaintenanceScheduleRequest { });
        return Ok(selectedMaintenanceSchedule);
    }
    [HttpGet]
    [Route("get-maintenanceScheduleListByDepartmentNameId")]

    public async Task<ActionResult> GetMaintenanceScheduleListByDepartmentNameId(int airCraftNameId, int departmentNameId)
    {
        var maintenanceSchedule = await _mediator.Send(new GetMaintenanceScheduleListByDepartmentNameIdRequest
        {
            AirCraftNameId= airCraftNameId,
            DepartmentNameId = departmentNameId
        });
        return Ok(maintenanceSchedule);
    }
    [HttpGet]
    [Route("get-selectedMaintenancePlanningByParametersFromSubCategory")]
    public async Task<ActionResult<List<SelectedModel>>> MaintenancePlanningByParametersFromSubCategory(int departmentNameId, int maintenanceCategoryId)
    {
        var subCategorys = await _mediator.Send(new GetSelectedMaintenancePlanningByParametersFromSubCategoryRequest
        {
            DepartmentNameId = departmentNameId,
            MaintenanceCategoryId = maintenanceCategoryId
        });
        return Ok(subCategorys);
    }
}

