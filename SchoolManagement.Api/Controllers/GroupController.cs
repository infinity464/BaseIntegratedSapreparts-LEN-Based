using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Group;
using SchoolManagement.Application.Features.Groups.Requests.Commands;
using SchoolManagement.Application.Features.Groups.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Group)]
[ApiController]
[Authorize]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Groups")]
    public async Task<ActionResult<List<GroupDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Groups = await _mediator.Send(new GetGroupListRequest { QueryParams = queryParams });
        return Ok(Groups);
    }


    [HttpGet]
    [Route("get-GroupDetail/{id}")]
    public async Task<ActionResult<GroupDto>> Get(int id)
    {
        var Group = await _mediator.Send(new GetGroupDetailRequest { GroupId = id });
        return Ok(Group);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Group")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGroupDto Group)
    {
        var command = new CreateGroupCommand { GroupDto = Group };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Group/{id}")]
    public async Task<ActionResult> Put([FromBody] GroupDto Group)
    {
        var command = new UpdateGroupCommand { GroupDto = Group };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Group/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteGroupCommand { GroupId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedGroups")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedGroup()
    {
        var Group = await _mediator.Send(new GetSelectedGroupRequest { });
        return Ok(Group);
    }
}

