using MediatR;
using SchoolManagement.Application.DTOs.JobCategory;

namespace SchoolManagement.Application.Features.JobCategorys.Requests.Commands
{
    public class UpdateJobCategoryCommand : IRequest<Unit>
    { 
        public JobCategoryDto JobCategoryDto { get; set; }
    }
}
 