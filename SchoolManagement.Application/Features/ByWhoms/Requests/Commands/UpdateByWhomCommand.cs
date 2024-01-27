using MediatR;
using SchoolManagement.Application.DTOs.ByWhom;

namespace SchoolManagement.Application.Features.ByWhoms.Requests.Commands
{
    public class UpdateByWhomCommand : IRequest<Unit>
    { 
        public ByWhomDto ByWhomDto { get; set; }
    }
}
 