using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.Features.AirCraftNames.Requests.Queries;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.CorrectiveMaintenence)]
[ApiController]
[Authorize]
public class CorrectiveMaintenenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public CorrectiveMaintenenceController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-correctiveMaintenences")]
    public async Task<ActionResult<List<CorrectiveMaintenenceDto>>> Get([FromQuery] QueryParams queryParams,int unitId, int departmentNameId, int issueRegisterId)
    {
        var CorrectiveMaintenences = await _mediator.Send(new GetCorrectiveMaintenenceListRequest
        { 
            QueryParams = queryParams,
            UnitId =unitId,
            DepartmentNameId =departmentNameId,
            IssueRegisterId =issueRegisterId
        });
        return Ok(CorrectiveMaintenences);
    }


    [HttpGet]
    [Route("get-correctiveMaintenenceDetail/{id}")]
    public async Task<ActionResult<CorrectiveMaintenenceDto>> Get(int id)
    {
        var CorrectiveMaintenence = await _mediator.Send(new GetCorrectiveMaintenenceDetailRequest { CorrectiveMaintenenceId = id });
        return Ok(CorrectiveMaintenence);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-correctiveMaintenence")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateCorrectiveMaintenenceDto CorrectiveMaintenence)
    {
        var command = new CreateCorrectiveMaintenenceCommand { CorrectiveMaintenenceDto = CorrectiveMaintenence };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-correctiveMaintenence/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateCorrectiveMaintenenceDto CorrectiveMaintenence)
    {
        var command = new UpdateCorrectiveMaintenenceCommand { CorrectiveMaintenenceDto = CorrectiveMaintenence };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-correctiveMaintenence/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCorrectiveMaintenenceCommand { CorrectiveMaintenenceId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedCorrectiveMaintenences")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedCorrectiveMaintenence()
    {
        var CorrectiveMaintenence = await _mediator.Send(new GetSelectedCorrectiveMaintenenceRequest { });
        return Ok(CorrectiveMaintenence);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("completed-corrective-maintenence/{id}")]
    public async Task<ActionResult> CompletedCorrectiveMaintenence([FromBody] CorrectiveMaintenenceCompletedDto correctiveMaintenence)
    {
        var command = new CorrectiveMaintenenceCompletedCommand { CorrectiveMaintenenceCompletedDto = correctiveMaintenence };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-spareStockByParameterSpRequest")]
    public async Task<ActionResult> GetSpareStockByParameterSpRequests(int unitId,int departmentNameId, int equipmentNameId)
    {
        var CourseTrainee = await _mediator.Send(new GetSpareStockByParameterSpRequest
        {
            UnitId = unitId,
            DepartmentNameId =departmentNameId,
            EquipmentNameId = equipmentNameId
        });
        return Ok(CourseTrainee);
    }




    [HttpGet]
    [Route("find-correctiveByParameters")]
    public async Task<ActionResult> GetCorrectiveByParameters(int? unitId, int? departmentId, int? itemCategoryId, int? equipmentNameId, int itemDetailId)
    {
        var presentStocks = await _mediator.Send(new GetCorrectiveByParametersSpRequest
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
    [Route("find-ongoingCorrectiveByParameters")]
    public async Task<ActionResult> GetOnGoingCorrectiveByParameters(int? unitId, int? departmentId, int? itemCategoryId, int? equipmentNameId, int itemDetailId)
    {
        var presentStocks = await _mediator.Send(new GetOngoingCorrectiveByParametersSpRequest
        {
            UnitId = unitId,
            DepartmentId = departmentId,
            ItemCategoryId = itemCategoryId,
            EquipmentNameId = equipmentNameId,
            ItemNameId = itemDetailId
        });
        return Ok(presentStocks);
    }

}

