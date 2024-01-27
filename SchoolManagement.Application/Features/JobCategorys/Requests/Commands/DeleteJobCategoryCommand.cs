using MediatR;

namespace SchoolManagement.Application.Features.JobCategorys.Requests.Commands
{
    public class DeleteJobCategoryCommand : IRequest
    {
        public int JobCategoryId { get; set; }
    }
} 
