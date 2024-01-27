using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.DTOs.PreventiveMaintenances;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.PreventiveMaintenance)]
[ApiController]
[Authorize]
public class PreventiveMaintenanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public PreventiveMaintenanceController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-PreventiveMaintenances")]
    public async Task<ActionResult<List<PreventiveMaintenanceDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var PreventiveMaintenances = await _mediator.Send(new GetPreventiveMaintenanceListRequest { QueryParams = queryParams });
        return Ok(PreventiveMaintenances);
    }


    [HttpGet]
    [Route("get-PreventiveMaintenanceDetail/{id}")]
    public async Task<ActionResult<PreventiveMaintenanceDto>> Get(int id)
    {
        var PreventiveMaintenance = await _mediator.Send(new GetPreventiveMaintenanceDetailRequest { PreventiveMaintenanceId = id });
        return Ok(PreventiveMaintenance);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-PreventiveMaintenance")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePreventiveMaintenanceDto PreventiveMaintenance)
    {
        var command = new CreatePreventiveMaintenanceCommand { PreventiveMaintenanceDto = PreventiveMaintenance };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-PreventiveMaintenance/{id}")]
    public async Task<ActionResult> Put([FromBody] PreventiveMaintenanceDto PreventiveMaintenance)
    {
        var command = new UpdatePreventiveMaintenanceCommand { PreventiveMaintenanceDto = PreventiveMaintenance };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-PreventiveMaintenance/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePreventiveMaintenanceCommand { PreventiveMaintenanceId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPreventiveMaintenances")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPreventiveMaintenance()
    {
        var PreventiveMaintenance = await _mediator.Send(new GetSelectedPreventiveMaintenanceRequest { });
        return Ok(PreventiveMaintenance);
    }
    [HttpGet]
    [Route("get-preventiveMaintenanceByDepartment")]
    public async Task<ActionResult> GetPreventiveMaintenanceByDepartment(int departmentNameId)
    {
        var maintenanceCategory = await _mediator.Send(new GetPreventiveMaintenanceDepartmentRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(maintenanceCategory);
    }

    [HttpGet]
    [Route("get-preventiveMaintenanceListByParameterRequest")]
    public async Task<ActionResult<List<PreventiveMaintenanceDto>>> GetPreventiveMaintenanceListByParameterRequest(int unitId, int departmentNameId, int maintenenceTypeId, int maintenanceCategoryId, int itemDetailId)
    {
        var preventive = await _mediator.Send(new GetPreventiveMaintenanceListByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            MaintenanceTypeId = maintenenceTypeId,
            MaintenanceCategoryId = maintenanceCategoryId,
            ItemDetailId = itemDetailId
        });
        return Ok(preventive);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("completed-preventive-maintenence/{id}")]
    public async Task<ActionResult> CompletedPreventiveMaintenence([FromBody] PreventiveMaintenanceCompletedDto preventiveMaintenence)
    {
        var command = new PreventiveMaintenenceCompletedCommand { PreventiveMaintenenceCompletedDto = preventiveMaintenence };
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpGet]
    [Route("get-preventiveListByDepartmentNameId")]
    public async Task<ActionResult> GetPreventiveListByDepartmentNameId(int departmentNameId, int unitId)
    {
        var equipmentName = await _mediator.Send(new GetPreventiveListByDepartmentNameIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(equipmentName);
    }
    [HttpGet]
    [Route("get-itemNameByUnitAndDepartmentIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetItemNameByUnitAndDepartmentId(int unitId, int departmentNameId)
    {
        var equipment = await _mediator.Send(new GetSelectItemNameByUnitAndDepartmentIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(equipment);
    }
    [HttpGet]
    [Route("find-preventiveListForMonitorintByParameters")]
    public async Task<ActionResult> GetPreventiveListForMonitorintByParameters(int? unitId, int? departmentId, int? itemDetailId,int?maintenenceCategoryId,int?maintenenceTypeId)
    {
        var presentStocks = await _mediator.Send(new GetPreventiveListForMonitorintByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            ItemDetailId = itemDetailId,
            MaintenanceCategoryId =maintenenceCategoryId,
            MaintenenceTypeId =maintenenceTypeId
        });
        return Ok(presentStocks);
    }
    [HttpGet]
    [Route("get-categoryByUnitAndDepartmentAndItemDetailIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetCategoryByUnitAndDepartmentId(int unitId, int departmentNameId, int itemDetailId)
    {
        var equipment = await _mediator.Send(new GetSelectCategoryByUnitAndDepartmentAndItemDetailIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemDetailId= itemDetailId
        });
        return Ok(equipment);

    }
    [HttpGet]
    [Route("get-subCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSubCategoryByUnitAndDepartmentId(int unitId, int departmentNameId, int itemDetailId, int maintenanceTypeId)
    {
        var equipment = await _mediator.Send(new GetSelectSubCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemDetailId = itemDetailId,
            MaintenanceTypeId = maintenanceTypeId
        });
        return Ok(equipment);

    }
    [HttpGet]
    [Route("get-selectedPmsNoByItemDetailId")]
    public async Task<ActionResult<List<SelectedModel>>> GetPmsNoByItemDetailId(int itemDetailId)
    {
        var pmsNo = await _mediator.Send(new GetPmsNoByItemDetailIdRequest
        {
            ItemDetailId = itemDetailId
        });
        return Ok(pmsNo);
    }

}