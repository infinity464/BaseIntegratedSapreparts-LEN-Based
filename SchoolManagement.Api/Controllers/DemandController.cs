using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Demands;
using SchoolManagement.Application.Features.Demands.Requests.Commands;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Application.Features.TraineeBioDataGeneralInfos.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Demand)]
[ApiController]
[Authorize]
public class DemandController : ControllerBase
{
    private readonly IMediator _mediator;

    public DemandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-demands")]
    public async Task<ActionResult<List<DemandDto>>> Get([FromQuery] QueryParams queryParams, int sparesCategoryId)
    {
        var Demands = await _mediator.Send(new GetDemandListRequest
        {
            QueryParams = queryParams,
            SparesCategoryId=sparesCategoryId
        });
        return Ok(Demands);
    }

    [HttpGet]
    [Route("get-demandDetail/{id}")]
    public async Task<ActionResult<DemandDto>> Get(int id)
    {
        var Demand = await _mediator.Send(new GetDemandDetailRequest { DemandId = id });
        return Ok(Demand);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-demand")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateDemandDto Demand)
    {
        var command = new CreateDemandCommand { DemandDto = Demand };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-demand/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateDemandDto Demand)
    {
        var command = new UpdateDemandCommand { CreateDemandDto = Demand };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-demand/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDemandCommand { DemandId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedDemand")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemand()
    {
        var Demand = await _mediator.Send(new GetSelectedDemandRequest { });
        return Ok(Demand); 
    }

    [HttpGet]
    [Route("get-itemNameByIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemand(int itemDetailId)
    {
        var itemName = await _mediator.Send(new GetItemNameByIdRequest
        {
            ItemDetailId = itemDetailId
        });
        return Ok(itemName); 
    }

    [HttpGet]
    [Route("get-autocompletePartNoByName")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompletePartNoByName(string partNo)
    {
        var course = await _mediator.Send(new GetAutoCompletePartNoRequest
        {
            PartNo = partNo,
        });
        return Ok(course);
    }

    [HttpGet]
    [Route("get-partnoFromDemandByDepartmentNameId")]
    public async Task<ActionResult<List<SelectedModel>>> GetPartnoFromDemandByDepartmentNameId(int departmentNameId, int sparesCategoryId)
    {
        var partNos = await _mediator.Send(new GetSelectedPartNoFromDemandByDepartmentNameRequest
        {
            DepartmentNameId = departmentNameId,
            SparesCategoryId = sparesCategoryId
        });
        return Ok(partNos);
    }

    [HttpGet]
    [Route("get-partnoFromDemandForUpdateByDepartmentNameId")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPartNoFromDemandForUpdateByDepartmentName(int departmentNameId, int sparesCategoryId)
    {
        var partNos = await _mediator.Send(new GetSelectedPartNoFromDemandForUpdateByDepartmentNameRequest
        {
            DepartmentNameId = departmentNameId,
            SparesCategoryId = sparesCategoryId
        });
        return Ok(partNos);
    }

    [HttpGet]
    [Route("get-selectedDemandById")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandByIdRequest(int demandId)
    {
        var demandLists = await _mediator.Send(new GetSelectedDemandByIdRequest
        {
            DemandId = demandId
        });
        return Ok(demandLists);
    }
    [HttpGet]
    [Route("get-DemandListByDepartmentNameId")]
    public async Task<ActionResult> GetDemandListByDepartmentNameId([FromQuery] QueryParams queryParams,int sparesCategoryId, int departmentNameId)
    {
        var demand = await _mediator.Send(new GetDemandListByDepartmentNameIdRequest
        {
            QueryParams = queryParams,
            DepartmentNameId = departmentNameId,
            SparesCategoryId= sparesCategoryId
        });
        return Ok(demand);
    }
    [HttpGet]
    [Route("get-DemandListForSparesByDepartmentNameId")]
    public async Task<ActionResult> GetDemandListForSparesByDepartmentNameId([FromQuery] QueryParams queryParams, int unitId, int departmentNameId, int itemCategoryId)
    {
        var demand = await _mediator.Send(new GetDemandListForSparesByDepartmentNameIdRequest
        {
            QueryParams = queryParams,
            DepartmentNameId = departmentNameId,
            UnitId = unitId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(demand);
    }
    [HttpGet]
    [Route("get-PartNoPassItemCategoryIdInDemand")]
    public async Task<ActionResult<List<SelectedModel>>> GetPartNoPassItemCategoryIdInDemand(int itemDetailId)
    {
        var partNos = await _mediator.Send(new GetSelectedPartNoPassItemCategoryIdInDemandRequest
        {
            ItemDetailId = itemDetailId
        });
        return Ok(partNos);
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
    [Route("get-partnoFromDemandByItemStatusIdAndProcurementStatus")] 
    public async Task<ActionResult<List<SelectedModel>>> GetPartnoFromDemandByItemStatusIdAndProcurementStatus(int itemStatusId, int procurementStatus)
    {
        var partNos = await _mediator.Send(new GetSelectedPartNoFromDemandByItemStatusAndProcureStatusRequest
        {
            ItemStatusId =itemStatusId,
            ProcurementStatus =procurementStatus
        });
        return Ok(partNos);
    }

    [HttpGet]
    [Route("get-selectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdRequest(int unitId, int departmentNameId, int itemCategoryId,int storeStatus, int procurementStatus)
    {
        var demandNo = await _mediator.Send(new GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            StoreStatus = storeStatus,
            ProcurementStatus = procurementStatus,
        });
        return Ok(demandNo); 
    }

    [HttpGet]
    [Route("get-selectedDateFromDemandByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDateFromDemandByParameterRequest(int unitId, int departmentNameId, int itemCategoryId, int demandId)
    {
        var date = await _mediator.Send(new GetSelectedDateFromDemandByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            DemandId =demandId
        });
        return Ok(date);
    }

    [HttpGet]
    [Route("get-selectedDemandQtyFromDemandByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetDemandQtyFromDemandByParameterRequest(int unitId, int departmentNameId, int itemCategoryId, int demandId)
    {
        var date = await _mediator.Send(new GetSelectedDemandQtyFromDemandByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            DemandId = demandId
        });
        return Ok(date);
    }

    [HttpGet] 
    [Route("get-selectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest(int unitId, int departmentNameId,int itemCategoryId,int storeStatus, int sftStatus)
    {
        var demandNo = await _mediator.Send(new GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest
        {
            UnitId = unitId, 
            DepartmentNameId = departmentNameId,
            ItemCategoryId =itemCategoryId,
            StoreStatus =storeStatus,
            SftStatus =sftStatus
          //  Status =status
        });
        return Ok(demandNo);
    }


    [HttpGet]
    [Route("get-selectedDemandNoForStoreByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNoForStoreByParameterRequest(int unitId, int departmentNameId,int storeStatus, int itemCategoryId)
    {
        var demandNo = await _mediator.Send(new GetSelectedDemandNoForStoreByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            StoreStatus = storeStatus
        });
        return Ok(demandNo);
    }


    [HttpGet]
    [Route("get-selectedRefNoByDemandId")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedRefNoByDemandId(int demandId)
    {
        var refNo = await _mediator.Send(new GetSelectedRefNoByDemandIdRequest
        {
           DemandId =demandId
        });
        return Ok(refNo);
    }

    [HttpGet]
    [Route("get-selectedDemandNo")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNo(int unitId,int departmentId)
    {
        var refNo = await _mediator.Send(new GetSelectedDemandNoRequest
        {
           UnitId = unitId,
           DepartmentId = departmentId
        });
        return Ok(refNo);
    }


    [HttpGet]
    [Route("find-demandStates")]
    public async Task<ActionResult> FindDemandStates(int unitId,int departmentId)
    {
        var FlyingTimeByAricraft = await _mediator.Send(new FindDemandStatesSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId
        });
        return Ok(FlyingTimeByAricraft);
    }


    [HttpGet]
    [Route("find-demandsByParameters")]
    public async Task<ActionResult> GetDemandByParameters(int? unitId, int? departmentId, int? itemCategoryId, int? equipmentNameId, int itemDetailId)
    {
        var presentStocks = await _mediator.Send(new GetDemandsByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId,
            ItemNameId = itemDetailId
        });
        return Ok(presentStocks);
    }

    [HttpGet]
    [Route("get-demandProcurementAndSftbyParameterSpRequest")]
    public async Task<ActionResult> GetDemandProcurementAndSftbyParameterSpRequest(int demandId)
    {
        var demandInfo = await _mediator.Send(new GetDemandProcurementAndSftBySpRequest
        {
           DemandId =demandId
        }); 
        return Ok(demandInfo);
    }



    [HttpGet]
    [Route("get-selectedDemandNoByItemStatus")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNoByItemStatusRequest(int unitId, int departmentNameId, int itemCategoryId, int itemStatusId, int status)
    {
        var demandNo = await _mediator.Send(new GetSelectedDemandNoByItemStatusByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            ItemStatusId = itemStatusId,
            Status = status
        });
        return Ok(demandNo);
    }

    [HttpGet]
    [Route("get-selectedDemandNoByDeamndId")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNoDemandId(int demandId)
    {
        var demandNo = await _mediator.Send(new GetSelectedDemandNoByDemandIdRequest
        {
            DemandId = demandId
        }); ;
        return Ok(demandNo);
    }
    [HttpGet]
    [Route("get-equipmentByUnitAndDepartmentAndCategoryIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetEquipmentNameByUnitAndDepartmentAndCategoryId(int unitId, int departmentNameId, int itemCategoryId)
    {
        var equipment = await _mediator.Send(new GetEquipmenteByUnitAndDepartmentAndCategoryIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(equipment);
    }
    [HttpGet]
    [Route("get-nameofItemByUnitAndDepartmentAndCategoryIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetNameofItemByUnitAndDepartmentAndCategoryId(int unitId, int departmentNameId, int itemCategoryId, int equipmentNameId)
    {
        var equipment = await _mediator.Send(new GetNameofItemByUnitAndDepartmentAndCategoryIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(equipment);
    }

    [HttpGet]
    [Route("get-selectedDemandNoByUnitAndDepartmentAndCategoryIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDemandNoByUnitAndDepartmentAndCategoryId(int unitId, int departmentNameId, int itemCategoryId, int equipmentNameId)
    {
        var equipment = await _mediator.Send(new GetSelectedDemandNoByUnitAndDepartmentAndCategoryIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(equipment);
    }

}

