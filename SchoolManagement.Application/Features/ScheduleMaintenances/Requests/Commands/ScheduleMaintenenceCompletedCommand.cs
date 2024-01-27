using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Commands
{
    public class ScheduleMaintenenceCompletedCommand : IRequest<Unit>
    {
        public ScheduleMaintenenceCompletedDto ScheduleMaintenenceCompletedDto { get; set; }
    }
}
      