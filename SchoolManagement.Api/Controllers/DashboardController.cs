using SchoolManagement.Application;
using SchoolManagement.Application.Features.Acceptances.Requests.Queries;
using SchoolManagement.Application.Features.AirCraftFlyings.Requests.Queries;
using SchoolManagement.Application.Features.AirCraftNames.Requests.Queries;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;
using SchoolManagement.Application.Features.ItemStors.Requests.Queries;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Application.Features.TrainingCrews.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Dashboard)]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-pendingDemands")]
    public async Task<ActionResult> GetPendingDemand(int departmentId)
    {
        var PendingDemand = await _mediator.Send(new GetPendingDemandSpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(PendingDemand);
    }

    [HttpGet]
    [Route("get-pendingProcurements")]
    public async Task<ActionResult> GetPendingProcurements(int departmentId)
    {
        var pendingProcurements = await _mediator.Send(new GetPendingProcurementSpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(pendingProcurements);
    }

    [HttpGet]
    [Route("get-pendingAcceptances")]
    public async Task<ActionResult> GetPendingAcceptances(int departmentId)
    {
        var acceptanceProcurements = await _mediator.Send(new GetPendingAcceptanceSpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(acceptanceProcurements);
    }


    [HttpGet]
    [Route("get-availableQty")]
    public async Task<ActionResult> GetAvailableQty(int departmentId)
    {
        var availableQty = await _mediator.Send(new GetAvailableQtySpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(availableQty);
    }

    [HttpGet]
    [Route("get-flyingTimeByAricraft")]
    public async Task<ActionResult> GetFlyingTimeByAricraft(int departmentId)
    {
        var FlyingTimeByAricraft = await _mediator.Send(new GetFlyingTimeByAricraftSpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(FlyingTimeByAricraft);
    }

    [HttpGet]
    [Route("get-aricraftFlying")]
    public async Task<ActionResult> GetFlyingTimeByAricraft(DateTime currentDate,int departmentId)
    {
        var FlyingTimeByAricraft = await _mediator.Send(new GetAirCraftFlyingSpRequest
        {
            Current = currentDate,
            DepartmentId = departmentId
        });
        return Ok(FlyingTimeByAricraft);
    }

    [HttpGet]
    [Route("get-trainingCrew")]
    public async Task<ActionResult> GetTrainingCrew(int departmentId)
    {
        var TrainingCrew = await _mediator.Send(new GetTrainingCrewSpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(TrainingCrew);
    }

    [HttpGet]
    [Route("get-remainProcurementQty")]
    public async Task<ActionResult> GetRemainProcurementQty(int departmentId)
    {
        var RemainProcurementQty = await _mediator.Send(new GetRemainProcurementQtySpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(RemainProcurementQty);
    }
    [HttpGet]
    [Route("get-SpGetCompleteStatus")]
    public async Task<ActionResult> GetSpGetCompleteStatus(int departmentId)
    {
        var FlyingTimeByAricraft = await _mediator.Send(new GetSpGetCompleteStatusRequest
        {
            DepartmentId = departmentId
        });
        return Ok(FlyingTimeByAricraft);
    }

    [HttpGet]
    [Route("get-equipmentStateListForDb")]
    public async Task<ActionResult> GetSpGetCompleteStatus(int unitId,int departmentId, int operationStatus)
    {
        var FlyingTimeByAricraft = await _mediator.Send(new GetEquipmentStateListBySpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            OperationStatus = operationStatus
        });
        return Ok(FlyingTimeByAricraft);
    }

    [HttpGet]
    [Route("get-spareEquipmentandToolsCount")]
    public async Task<ActionResult> GetSpareToolsAndEquipmentCount(int unitId, int departmentId, int itemCategoryId)
    {
        var itemdetails = await _mediator.Send(new GetSpareEquipmentandToolsCountSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(itemdetails);
    }

    [HttpGet]
    [Route("get-spareCount")]
    public async Task<ActionResult> GetSpareCount(int unitId, int departmentNameId, int itemCategoryId)
    {
        var equipment = await _mediator.Send(new GetSpareCountSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentNameId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(equipment);
    }

    [HttpGet]
    [Route("get-toolsCount")]
    public async Task<ActionResult> GetToolsCount(int unitId, int departmentNameId, int itemCategoryId)
    {
        var equipment = await _mediator.Send(new GetSpareCountSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentNameId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(equipment);
    }
}

