using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Commands
{
    public class UpdateSpareRequiredStatusCommand : IRequest<Unit>
    { 
        public SpareRequiredStatusDto SpareRequiredStatusDto { get; set; }
    }
}
 