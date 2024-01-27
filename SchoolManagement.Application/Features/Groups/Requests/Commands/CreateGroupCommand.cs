using MediatR;
using SchoolManagement.Application.DTOs.Group;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Groups.Requests.Commands
{
    public class CreateGroupCommand : IRequest<BaseCommandResponse>
    {
        public CreateGroupDto GroupDto { get; set; }
    }
}
