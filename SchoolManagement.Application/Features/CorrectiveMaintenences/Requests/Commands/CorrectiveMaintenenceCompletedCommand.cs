using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Commands
{
    public class CorrectiveMaintenenceCompletedCommand : IRequest<Unit>
    {
        public CorrectiveMaintenenceCompletedDto CorrectiveMaintenenceCompletedDto { get; set; }
    }
}
    