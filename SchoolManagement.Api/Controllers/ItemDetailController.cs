using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ItemDetail;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;
using SchoolManagement.Application.Features.ItemDetails.Requests.Commands;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using SchoolManagement.Shared.Models;


namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ItemDetail)]
[ApiController]
[Authorize]
public class ItemDetailController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemDetailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ItemDetails")]
    public async Task<ActionResult<List<ItemDetailDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ItemDetails = await _mediator.Send(new GetItemDetailListRequest { QueryParams = queryParams });
        return Ok(ItemDetails);
    }

    [HttpGet]
    [Route("get-itemDetailsForTools")]
    public async Task<ActionResult<List<ItemDetailDto>>> GetItemDetailsForTools([FromQuery] QueryParams queryParams,int sparesCategoryId)
    {
        var ItemDetails = await _mediator.Send(new GetItemDetailListForToolsRequest
        { 
            QueryParams = queryParams,
            SparesCategoryId =sparesCategoryId
            
        });
        return Ok(ItemDetails);
    }

    [HttpGet]
    [Route("get-ItemDetailDetail/{id}")]
    public async Task<ActionResult<ItemDetailDto>> Get(int id)
    {
        var ItemDetail = await _mediator.Send(new GetItemDetailDetailRequest { ItemDetailId = id });
        return Ok(ItemDetail);
    }

    

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ItemDetail")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateItemDetailDto ItemDetail)
    {
        var command = new CreateItemDetailCommand { ItemDetailDto = ItemDetail };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ItemDetail/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateItemDetailDto ItemDetail)
    {
        var command = new UpdateItemDetailCommand { CreateItemDetailDto = ItemDetail };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ItemDetail/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteItemDetailCommand { ItemDetailId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedItemDetails")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedItemDetail()
    {
        var selectedItemDetail = await _mediator.Send(new GetSelectedItemDetailRequest { });
        return Ok(selectedItemDetail);
    }

    [HttpGet] 
    [Route("get-partnoByDepartmentNameId")]
    public async Task<ActionResult<List<SelectedModel>>> GetPartnoByDepartmentNameId(int departmentNameId,int itemCategoryId)
    {
        var partNos = await _mediator.Send(new GetSelectedPartNoByDepartmentNameRequest 
        { 
            DepartmentNameId = departmentNameId,
            ItemCategoryId =itemCategoryId
        });
        return Ok(partNos); 
    } 

    [HttpGet]
    [Route("get-itemNameByItemDetailId")] 
    public async Task<ActionResult<List<SelectedModel>>> GetItemNameByItemDetailId(int itemDetailId)
    {
        var partNos = await _mediator.Send(new GetSelectedItemNameByItemDetailIdRequest
        {
            ItemDetailId = itemDetailId 
        });
        return Ok(partNos);
    }

    [HttpGet]
    [Route("get-selectedItemDetailListByDepartmentId")]
    public async Task<ActionResult<List<ItemDetailDto>>> selectedItemDetailListByDepartmentId(int departmentNameId)
    {
        var itemDetail = await _mediator.Send(new GetItemDetailListByDepartmentIdRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(itemDetail);

    }
    [HttpGet]
    [Route("get-ItemDetailByDepartmentId")]
    public async Task<ActionResult<List<ItemDetailDto>>> GetItemDetailByDepartmentId(int departmentNameId)
    {
        var itemdetails = await _mediator.Send(new GetItemDetailByDepartmentIdRequest
        {
            DepartmentNameId = departmentNameId,
        });
        return Ok(itemdetails);
    }

    [HttpGet]
    [Route("get-itemDetailByDepartmentIdUnitIdItemCategoryId")]
    public async Task<ActionResult<List<ItemDetailDto>>> GetItemDetailByDepartmentIdUnitIdItemCategoryId([FromQuery] QueryParams queryParams, int departmentNameId,int unitId,int itemCategoryId)
    {
        var itemdetails = await _mediator.Send(new GetItemDetailListByDepartmentIdUnitIdAndItemCategoryIdRequest
        {
            QueryParams = queryParams,
            DepartmentNameId = departmentNameId,
            UnitId =unitId,
            ItemCategoryId =itemCategoryId
        });
        return Ok(itemdetails);
    }

    

    [HttpGet]
    [Route("get-autocompletePartNoByNameForSpares")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompletePartNoForSpares(string partNo,int unitId,int departmentNameId, int itemCategoryId)
    {
        var course = await _mediator.Send(new GetAutoCompletePartNoForSparesRequest 
        {
            PartNo = partNo,
            UnitId =unitId,
            DepartmentNameId =departmentNameId,
            ItemCategoryId =itemCategoryId
        });
        return Ok(course);

    }

    [HttpGet]
    [Route("get-autocompleteImcByNameForSpares")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteImcForSpares(string imc, int unitId, int departmentNameId, int itemCategoryId)
    {
        var course = await _mediator.Send(new GetAutoCompleteImcForSparesRequest
        {
            Imc = imc,
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(course);
    }

    [HttpGet]
    [Route("get-autocompletePartNoByDepartment")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompletePartNoForDepartment(string partNo)
    {
        var itemDetail = await _mediator.Send(new GetAutoCompletePartNoByDepartmentRequest
        {
            PartNo = partNo,
        });
        return Ok(itemDetail);

    }


    [HttpGet]
    [Route("get-presentStocks")]
    public async Task<ActionResult> GetPresentStocks(int departmentId)
    {
        var presentStocks = await _mediator.Send(new GetPresentStockSpRequest
        {
            DepartmentId = departmentId
        });
        return Ok(presentStocks);
    }


    [HttpGet]
    [Route("find-itemDetailsByParameters")]
    public async Task<ActionResult> GetItemDetailByParameters(int? unitId,int? departmentId, int? itemCategoryId, int? equipmentNameId)
    {
        var presentStocks = await _mediator.Send(new GetItemDetailByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(presentStocks);
    }



    [HttpGet]
    [Route("get-selecteditemNameByParameters")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedItemNameByParameters(int unitId, int departmentId, int itemCategoryId, int equipmentNameId)
    {
        var itemNames = await _mediator.Send(new GetSelectedItemNameByParametersRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(itemNames);
    }

    [HttpGet]
    [Route("get-selecteditemNameByParametersForPreventive")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedItemNameByParametersForPreventive(int unitId, int departmentId)
    {
        var itemNames = await _mediator.Send(new GetSelectedItemNameByParametersForPreventiveRequest
        { 
            UnitId = unitId,
            DepartmentNameId = departmentId, 
          //  ItemCategoryId = itemCategoryId,
        });
        return Ok(itemNames);
    }

    [HttpGet]
    [Route("get-ItemNameByItemCategoryDepartmentNameId")]
    public async Task<ActionResult<List<SelectedModel>>> GetItemNameByItemCategoryDepartmentNameId(int departmentNameId)
    {
        var partNos = await _mediator.Send(new GetSelectedItemNameByItemCategoryDepartmentNameRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(partNos);
    }
    [HttpGet]
    [Route("get-ItemNameByItemCategoryAndDepartmentNameId")]
    public async Task<ActionResult<List<SelectedModel>>> GetItemNameByItemCategoryAndDepartmentNameId(int departmentNameId)
    {
        var partNos = await _mediator.Send(new GetSelectedItemNameByItemCategoryAndDepartmentNameRequest
        {
            DepartmentNameId = departmentNameId
        });
        return Ok(partNos);
    }


    [HttpGet]
    [Route("find-availableQtyByParameters")]
    public async Task<ActionResult> GetAvailableQtyByParameters(int? unitId, int? departmentId, int? itemCategoryId, int? equipmentNameId)
    {
        var presentStocks = await _mediator.Send(new GetAvailableQtyByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(presentStocks);
    }

    [HttpGet]
    [Route("get-selectedPmsNoByItemDetailId")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPmsNoByItemDetailId(int itemDetailId)
    {
        var pmsNo = await _mediator.Send(new GetSelectedPmsNoByItemDetailIdRequest
        {
           ItemDetailId =itemDetailId
        });
        return Ok(pmsNo);
    }
    [HttpGet]
    [Route("get-equipmentNameByUnitAndDepartmentAndCategoryIdRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetEquipmentNameByUnitAndDepartmentAndCategoryId(int unitId, int departmentNameId, int itemCategoryId)
    {
        var equipment = await _mediator.Send(new GetEquipmentNameByUnitAndDepartmentAndCategoryIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId
        });
        return Ok(equipment);
    }

  



}

