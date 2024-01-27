using MediatR;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands
{
    public class DeleteCorrectiveMaintenenceCommand : IRequest
    {
        public int CorrectiveMaintenenceId { get; set; }
    }
} 
