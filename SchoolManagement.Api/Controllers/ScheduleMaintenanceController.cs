using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Handlers.Commands;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ScheduleMaintenance)]
[ApiController]
[Authorize]
public class ScheduleMaintenanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScheduleMaintenanceController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-scheduleMaintenances")]
    public async Task<ActionResult<List<ScheduleMaintenanceDto>>> Get([FromQuery] QueryParams queryParams, int unitId, int departmentNameId, int issueRegisterId)
    {
        var ScheduleMaintenances = await _mediator.Send(new GetScheduleMaintenanceListRequest
        { 
            QueryParams = queryParams,
            UnitId =unitId,
            DepartmentNameId =departmentNameId,
            IssueRegisterId =issueRegisterId
        });
        return Ok(ScheduleMaintenances);
    }



    [HttpGet]
    [Route("get-scheduleMaintenancesListByDept")]
    public async Task<ActionResult<List<ScheduleMaintenanceDto>>> Get([FromQuery] QueryParams queryParams, int unitId, int departmentNameId)
    {
        var ScheduleMaintenances = await _mediator.Send(new GetScheduleMaintenanceListByDeptRequest
        {
            QueryParams = queryParams,
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(ScheduleMaintenances);
    }


    [HttpGet]
    [Route("get-scheduleMaintenanceDetail/{id}")]
    public async Task<ActionResult<ScheduleMaintenanceDto>> Get(int id)
    {
        var ScheduleMaintenance = await _mediator.Send(new GetScheduleMaintenanceDetailRequest { ScheduleMaintenanceId = id });
        return Ok(ScheduleMaintenance);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-scheduleMaintenance")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateScheduleMaintenanceDto ScheduleMaintenance)
    {
        var command = new CreateScheduleMaintenanceCommand { ScheduleMaintenanceDto = ScheduleMaintenance };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType] 
    [Route("completed-schedule-maintenence/{id}")]
    public async Task<ActionResult> CompletedScheduleMaintenence([FromBody] ScheduleMaintenenceCompletedDto scheduleMaintenence)
    {
        var command = new ScheduleMaintenenceCompletedCommand { ScheduleMaintenenceCompletedDto = scheduleMaintenence};
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-scheduleMaintenance/{id}")]
    public async Task<ActionResult> Put([FromBody] ScheduleMaintenanceDto ScheduleMaintenance)
    {
        var command = new UpdateScheduleMaintenanceCommand { ScheduleMaintenanceDto = ScheduleMaintenance };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-scheduleMaintenance/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteScheduleMaintenanceCommand { ScheduleMaintenanceId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedScheduleMaintenance")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedScheduleMaintenance()
    {
        var ScheduleMaintenance = await _mediator.Send(new GetSelectedScheduleMaintenanceRequest { });
        return Ok(ScheduleMaintenance);
    }



    [HttpGet]
    [Route("find-scheduleMaintenancesByParameters")]
    public async Task<ActionResult> GetProcurementByParameters(int unitId, int departmentId, int issueRegisterId, int scheduleMaintenenceTypeId, int fiscalYearId)
    {
        var presentStocks = await _mediator.Send(new GetScheduledMaintanenceByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            IssueRegisterId = issueRegisterId,
            ScheduleMaintenenceTypeId = scheduleMaintenenceTypeId,
            FiscalYearId = fiscalYearId
        });
        return Ok(presentStocks);
    }

    [HttpGet]
    [Route("find-scheduleMaintenancesCountByParameters")]
    public async Task<ActionResult> GetScheduleJobCountByParameters(int unitId, int departmentId, int issueRegisterId, int scheduleMaintenenceTypeId, int fiscalYearId)
    {
        var presentStocks = await _mediator.Send(new GetTotalJobScheduleCountByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            IssueRegisterId = issueRegisterId,
            ScheduleMaintenenceTypeId = scheduleMaintenenceTypeId,
            FiscalYearId = fiscalYearId
        });
        return Ok(presentStocks);
    }

    [HttpGet]
    [Route("find-scheduleTotalParameters")]
    public async Task<ActionResult> GetTotalJobCountByParameters(int unitId, int departmentId, int issueRegisterId, int scheduleMaintenenceTypeId, int fiscalYearId)
    {
        var presentStocks = await _mediator.Send(new GetTotalJobCountByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            IssueRegisterId = issueRegisterId,
            ScheduleMaintenenceTypeId = scheduleMaintenenceTypeId,
            FiscalYearId = fiscalYearId
        });
        return Ok(presentStocks);
    }
}

