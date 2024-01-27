using MediatR;
using SchoolManagement.Application.DTOs.ByWhom;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ByWhoms.Requests.Commands
{
    public class CreateByWhomCommand : IRequest<BaseCommandResponse>
    {
        public CreateByWhomDto ByWhomDto { get; set; }
    }
}
