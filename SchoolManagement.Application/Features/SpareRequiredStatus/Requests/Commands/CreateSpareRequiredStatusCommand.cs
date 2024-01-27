using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Commands
{
    public class CreateSpareRequiredStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreateSpareRequiredStatusDto SpareRequiredStatusDto { get; set; }
    }
}
