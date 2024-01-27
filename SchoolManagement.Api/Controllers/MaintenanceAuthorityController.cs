using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.MaintenanceAuthority)]
[ApiController]
[Authorize]
public class MaintenanceAuthorityController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceAuthorityController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-MaintenanceAuthoritys")]
    public async Task<ActionResult<List<MaintenanceAuthorityDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var MaintenanceAuthoritys = await _mediator.Send(new GetMaintenanceAuthorityListRequest { QueryParams = queryParams });
        return Ok(MaintenanceAuthoritys);
    }


    [HttpGet]
    [Route("get-MaintenanceAuthorityDetail/{id}")]
    public async Task<ActionResult<MaintenanceAuthorityDto>> Get(int id)
    {
        var MaintenanceAuthority = await _mediator.Send(new GetMaintenanceAuthorityDetailRequest { MaintenanceAuthorityId = id });
        return Ok(MaintenanceAuthority);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-MaintenanceAuthority")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateMaintenanceAuthorityDto MaintenanceAuthority)
    {
        var command = new CreateMaintenanceAuthorityCommand { MaintenanceAuthorityDto = MaintenanceAuthority };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-MaintenanceAuthority/{id}")]
    public async Task<ActionResult> Put([FromBody] MaintenanceAuthorityDto MaintenanceAuthority)
    {
        var command = new UpdateMaintenanceAuthorityCommand { MaintenanceAuthorityDto = MaintenanceAuthority };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-MaintenanceAuthority/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteMaintenanceAuthorityCommand { MaintenanceAuthorityId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedMaintenanceAuthoritys")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedMaintenanceAuthority()
    {
        var MaintenanceAuthority = await _mediator.Send(new GetSelectedMaintenanceAuthorityRequest { });
        return Ok(MaintenanceAuthority);
    }
}

