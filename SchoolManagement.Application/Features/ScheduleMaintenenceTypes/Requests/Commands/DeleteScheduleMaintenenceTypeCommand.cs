using MediatR;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands
{
    public class DeleteScheduleMaintenenceTypeCommand : IRequest
    {
        public int ScheduleMaintenenceTypeId { get; set; }
    }
} 
