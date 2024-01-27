using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ItemStor;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Requests.Queries;
using SchoolManagement.Shared.Models;


namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ItemStor)]
[ApiController]
[Authorize]
public class ItemStorController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemStorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ItemStors")]
    public async Task<ActionResult<List<ItemStorDto>>> Get([FromQuery] QueryParams queryParams, int itemCategoryId)
    {
        var ItemStors = await _mediator.Send(new GetItemStorListRequest { QueryParams = queryParams, ItemCategoryId = itemCategoryId });
        return Ok(ItemStors);
    }
     
    [HttpGet]
    [Route("get-itemStorListByParameterRequest")]
    public async Task<ActionResult<List<ItemStorDto>>> GetItemStorByParameterRequest([FromQuery] QueryParams queryParams, int departmentNameId,int unitId, int itemCategoryId)
    {
        var ItemStors = await _mediator.Send(new GetItemStorListByParameterRequest 
        { 
            QueryParams = queryParams, 
            DepartmentNameId=departmentNameId,
            ItemCategoryId =itemCategoryId,
            UnitId = unitId,
        });
        return Ok(ItemStors);
    } 

    [HttpGet]
    [Route("get-ItemStorDetail/{id}")]
    public async Task<ActionResult<ItemStorDto>> Get(int id)
    {
        var ItemStor = await _mediator.Send(new GetItemStorDetailRequest { ItemStorId = id });
        return Ok(ItemStor);
    }

    

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ItemStor")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateItemStorDto ItemStor)
    {
        var command = new CreateItemStorCommand { ItemStorDto = ItemStor };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-PreviousItemStor")]
    public async Task<ActionResult<BaseCommandResponse>> PreviousItemStorePost([FromForm] CreateItemStorDto ItemStor)
    {
        var command = new CreatePreviousItemStoreCommand { ItemStorDto = ItemStor };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ItemStor/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateItemStorDto ItemStor)
    {
        var command = new UpdateItemStorCommand { ItemStorDto = ItemStor };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ItemStor/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteItemStorCommand { ItemStorId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedItemStors")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedItemStor()
    {
        var selectedItemStor = await _mediator.Send(new GetSelectedItemStorRequest { });
        return Ok(selectedItemStor);
    }

    [HttpGet]
    [Route("get-itemStoreListForIssueRegisterByDepartmentNameIdAndSparesCategoryIdandItemDetail")]
    public async Task<ActionResult<List<ItemStorDto>>> GetItemStoreListForIssueRegisterByDepartmentNameIdAndSparesCategoryIdandItemDetail(int departmentNameId,int sparesCategoryId,int itemDetailId)
    {
        var ItemStors = await _mediator.Send(new GetItemStoreListForItemRegisterByDepartmentNameIdAndSpareCategoryIdandItemDetailRequest
        {
            DepartmentNameId=departmentNameId,
            SparesCategoryId =sparesCategoryId,
            ItemDetailId =itemDetailId
        });
        return Ok(ItemStors);
    }

    [HttpGet]
    [Route("get-itemStoreListForIssueRegisterByDepartmentNameIdAndSparesCategoryId")]
    public async Task<ActionResult<List<ItemStorDto>>> GetItemStoreListForIssueRegisterByDepartmentNameIdAndSparesCategoryId(int departmentNameId, int sparesCategoryId)
    {
        var ItemStors = await _mediator.Send(new GetItemStoreListForItemRegisterByDepartmentNameIdAndSpareCategoryIdRequest
        {
            DepartmentNameId = departmentNameId,
            SparesCategoryId = sparesCategoryId,
        });
        return Ok(ItemStors);
    }

    [HttpGet]
    [Route("get-selectedPartNoByDepartmentNameIdAndSpareCategoryIdFromItemStore")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPartNoByDepartmentNameIdAndSpareCategoryIdFromItemStore(int departmentNameId,int spareCategoryId)
    {
        var selectedItemStor = await _mediator.Send(new GetSelectedItemDetailByDepartmentNameIdAndSpareCategoryIdFromItemStoreRequest 
        { 
         DepartmentNameId =departmentNameId,
         SparesCategoryId =spareCategoryId
        });
        return Ok(selectedItemStor); 
    }


    [HttpGet]
    [Route("get-selectedItemNameByDepartmentNameIdAndSpareCategoryIdItemDetailIdFromItemStore")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedItemNameByDepartmentNameIdAndSpareCategoryIdItemDetailIdFromItemStore(int departmentNameId, int spareCategoryId, int itemDetailId)
    {
        var selectedItemStor = await _mediator.Send(new GetSelectedItemDetailByDepartmentNameIdAndSpareCategoryIditemDetailIdFromItemStoreRequest
        {
            DepartmentNameId = departmentNameId,
            SparesCategoryId = spareCategoryId,
            ItemDetailId = itemDetailId
        });
        return Ok(selectedItemStor);
    }
    [HttpGet]
    [Route("get-ItemStorListByDepartmentNameId")]
    public async Task<ActionResult<List<ItemStorDto>>> GetItemStorListByDepartmentNameId([FromQuery] QueryParams queryParams, int departmentNameId)
    {
        var ItemStors = await _mediator.Send(new GetItemStorListByDepartmentNameIdRequest { QueryParams = queryParams, DepartmentNameId = departmentNameId });
        return Ok(ItemStors);
    }

    //[HttpGet]
    //[Route("get-PreviousItemStores")]
    //public async Task<ActionResult<List<ItemStorDto>>> GetPrevious([FromQuery] QueryParams queryParams)
    //{
    //    var ItemStors = await _mediator.Send(new GetPreviousItemStoreListRequest { QueryParams = queryParams});
    //    return Ok(ItemStors);
    //}
}

