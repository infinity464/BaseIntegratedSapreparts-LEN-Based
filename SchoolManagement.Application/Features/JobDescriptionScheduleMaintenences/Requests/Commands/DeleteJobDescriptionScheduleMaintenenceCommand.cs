using MediatR;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands
{
    public class DeleteJobDescriptionScheduleMaintenenceCommand : IRequest
    {
        public int JobDescriptionScheduleMaintenenceId { get; set; }
    }
} 
