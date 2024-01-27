using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.IssueRegister.MultipleInsertDto;
using SchoolManagement.Application.DTOs.ItemStor;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Shared.Models;


namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.IssueRegister)]
[ApiController]
[Authorize]
public class IssueRegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueRegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-IssueRegisters")]
    public async Task<ActionResult<List<IssueRegisterDto>>> Get([FromQuery] QueryParams queryParams,int unitId,int departmentId)
    {
        var IssueRegisters = await _mediator.Send(new GetIssueRegisterListRequest 
        {
            QueryParams = queryParams,
            UnitId =unitId,
            DepartmentNameId =departmentId
        });
        return Ok(IssueRegisters);
    }

    [HttpGet]
    [Route("get-IssueRegisterDetail/{id}")]
    public async Task<ActionResult<IssueRegisterDto>> Get(int id)
    {
        var IssueRegister = await _mediator.Send(new GetIssueRegisterDetailRequest { IssueRegisterId = id });
        return Ok(IssueRegister);
    }

    

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-IssueRegister")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateIssueRegisterDto IssueRegister)
    {
        var command = new CreateIssueRegisterCommand { IssueRegisterDto = IssueRegister };
        var response = await _mediator.Send(command);
        return Ok(response); 
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-IssueRegister/{id}")]
    public async Task<ActionResult> Put([FromBody] IssueRegisterDto IssueRegister)
    {
        var command = new UpdateIssueRegisterCommand { IssueRegisterDto = IssueRegister };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [Route("update-IssueRegisterForSurvey")]
    public async Task<ActionResult> UpdateIssueRegisterForSurvey([FromBody] IssueRegisterDto IssueRegister)
    {
        var command = new UpdateIssueRegisterFromSurveyCommand { IssueRegisterDto = IssueRegister };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-IssueRegister/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteIssueRegisterCommand { IssueRegisterId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedIssueRegisters")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedIssueRegister()
    {
        var selectedIssueRegister = await _mediator.Send(new GetSelectedIssueRegisterRequest { });
        return Ok(selectedIssueRegister);
    }

    [HttpGet]
    [Route("get-selectedIssueRegisterList")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedIssueRegisterListRequest(int departmentNameId, int sparesCategoryId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetSelectedIssueRegisterListRequest
        {
            DepartmentNameId = departmentNameId,
            SparesCategoryId = sparesCategoryId
        });
        return Ok(selectedIssueRegister);
    }

    [HttpGet] 
    [Route("get-issueRegisterListFromStore")]
    public async Task<ActionResult<List<ItemStorDto>>> GetIssueRegisterListFromStore(int unitId,int departmentNameId,int itemCategoryId,int itemDetailId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetIssueRegisterListFromStoreRequest
        {
          UnitId =unitId,
          DepartmentNameId =departmentNameId,
          ItemCategoryId=itemCategoryId,
         // EquipmentNameId =equipmentNameId,
          ItemDetailId =itemDetailId
        });
        return Ok(selectedIssueRegister);
    }


    [HttpGet]
    [Route("get-issueRegisterListForEquipmentFromStore")]
    public async Task<ActionResult<List<ItemStorDto>>> GetIssueRegisterListForEquipmentFromStore(int unitId, int departmentNameId, int itemCategoryId, int itemDetailId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetIssueRegisterListForEquipmentFromStoreRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            ItemDetailId = itemDetailId
        });
        return Ok(selectedIssueRegister);
    }


    [HttpGet]
    [Route("get-issueRegisterListByParameterRequest")]
    public async Task<ActionResult<List<ItemStorDto>>> GetIssueRegisterListByParameterRequest(int unitId, int departmentNameId, int itemCategoryId, int issueTypeId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetIssueRegisterListByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId = itemCategoryId,
            IssueTypeId =issueTypeId
        });
        return Ok(selectedIssueRegister);
    }

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesDefaultResponseType] 
    //[Route("adding-returnQtyToStore/{id}")]
    //public async Task<ActionResult> AddReturnQtyToStore(int id)
    //{
    //    var command = new AddingReturnQtyRegisterCommand { ItemStoreId = id };
    //    await _mediator.Send(command);
    //    return NoContent();
    //}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("adding-returnQtyToStore/{id}")] 
    public async Task<ActionResult> AddReturnQtyToStore([FromBody] AddReturnQtyToStoreDto IssueRegister)
    {
        // [FromBody] IssueRegisterDto
        var command = new AddingReturnQtyRegisterCommand { IssueRegisterDto = IssueRegister };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedEquipmentNameByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEquipmentNameByParameterRequest(int unitId, int departmentNameId)
    {
        var equipment = await _mediator.Send(new GetSelectedEquipmentNameByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(equipment);
    }


    [HttpGet]
    [Route("get-issueRegisterListForSurvey")]
    public async Task<ActionResult<List<SelectedModel>>> GetIssueRegisterListForSurveyRequest(int unitId, int departmentNameId)
    {
        var equipment = await _mediator.Send(new GetIssueRegisterListForSurveyRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(equipment);
    }

    [HttpGet]
    [Route("get-selectedEquipmentNameForSurveyByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEquipmentNameForServeyByParameterRequest(int unitId, int departmentNameId)
    {
        var equipment = await _mediator.Send(new GetSelectedEquipmentNameForSurveyByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(equipment);
    }

    [HttpGet]
    [Route("get-selectedEquipmentNameFromIssueRegisterByParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEquipmentNameFromIssueRegisterByParameterRequest(int unitId, int departmentNameId, int itemCategoryId)
    {
        var equipment = await _mediator.Send(new GetSelectedEquipmentNameFromIssueRegisterByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId =itemCategoryId
        });
        return Ok(equipment);
    }


    [HttpGet]
    [Route("find-issueRegisterByParameters")]
    public async Task<ActionResult> GetIssueRegisterByParameters(int unitId, int departmentId, int issueTypeId)
    {
        var presentStocks = await _mediator.Send(new GetIssueRegisterByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            IssueTypeId = issueTypeId
        });
        return Ok(presentStocks);
    }


    [HttpGet]
    [Route("find-issueRegisterListForSurveyMonitoringByParameters")]
    public async Task<ActionResult> GetIssueRegisterListForSurveyMonitoringByParameters(int unitId, int departmentId,int itemCategoryId, int equipmentNameId)
    {
        var presentStocks = await _mediator.Send(new GetIssueRegisterListForSurveyMonitoringByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(presentStocks);
    }
    [HttpGet]
    [Route("get-issueRegisterListForSurveyDeclareByParameterRequest")]
    public async Task<ActionResult<List<ItemStorDto>>> GetIssueRegisterListForSurveyDeclareByParameterRequest(int unitId, int departmentNameId,int itemCategoryId,int itemDetailId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetIssueRegisterListForSurveyDeclareByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId,
            ItemCategoryId =itemCategoryId,
            ItemDetailId =itemDetailId
        });
        return Ok(selectedIssueRegister);
    }


    [HttpGet]
    [Route("update-survayStatusOnIssueRegister")]
    public async Task<ActionResult> UpdateSurvayStatusOnIssueRegisterRequest(int issueRegisterId, int status)
    {
        var selectedIssueRegister = await _mediator.Send(new UpdateIssueRegisterSurvayStatusCommand
        {
            IssueRegisterId = issueRegisterId,
            QtyAfterSurvey = status
        });
        return Ok(selectedIssueRegister);
	}
	
    [HttpGet]
    [Route("get-selectedSurveyNoForDemandParameterRequest")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSurveyNoForDemandRequest(int unitId, int departmentNameId)
    {
        var surveyNo = await _mediator.Send(new GetSelectedSurveyNoForDemandRegisterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(surveyNo);

    }
    [HttpGet]
    [Route("get-issueRegisterListForEquipmentNonOparatonalByParameterRequest")]
    public async Task<ActionResult<List<ItemStorDto>>> GetIssueRegisterListForEquipmentNonOparatonalyParameterRequest(int unitId, int departmentNameId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetIssueRegisterListForEquipmentNonOparatonalByParameterRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(selectedIssueRegister);
    }
    [HttpGet]
    [Route("update-equipNonOPLStatusOnIssueRegister")]
    public async Task<ActionResult> UpdateEqupNonOPLStatusRegisterRequest(int issueRegisterId, int status)
    {
        var selectedIssueRegister = await _mediator.Send(new UpdateIssueRegisterEqupNonOPLStatusCommand
        {
            IssueRegisterId = issueRegisterId,
            Status = status
        });
        return Ok(selectedIssueRegister);
    }
    [HttpGet]
    [Route("get-listForEquipNonOplBySurveyStatus")]
    public async Task<ActionResult<List<IssueRegisterDto>>> GetSelectedListForEquipNonOplBySurveyStatus(int unitId, int departmentNameId)
    {
        var surveyNo = await _mediator.Send(new GetIssueRegisterListForEquipNonOplBySurveyStatusRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(surveyNo);

    }
    [HttpGet]
    [Route("get-selectedItemCategoryByUnitAndDepartmentId")]
    public async Task<ActionResult<List<SelectedModel>>> GetItemCategoryByUnitAndDepartmentId(int unitId, int departmentNameId)
    {
        var category = await _mediator.Send(new GetItemCategoryByUnitAndDepartmentIdRequest
        {
            UnitId = unitId,
            DepartmentNameId = departmentNameId
        });
        return Ok(category);
    }
}

