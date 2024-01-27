using MediatR;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands
{
    public class DeleteJobCategoryMaintenenceCommand : IRequest
    {
        public int JobCategoryMaintenenceId { get; set; }
    }
} 
