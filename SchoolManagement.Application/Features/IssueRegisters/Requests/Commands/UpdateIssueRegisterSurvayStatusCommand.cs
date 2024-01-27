using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Commands
{
    public class UpdateIssueRegisterSurvayStatusCommand : IRequest
    {
        public int IssueRegisterId { get; set; }
        public int QtyAfterSurvey { get; set; }
    }
}
