using MediatR;
using SchoolManagement.Application.DTOs.JobCategory;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.JobCategorys.Requests.Commands
{
    public class CreateJobCategoryCommand : IRequest<BaseCommandResponse>
    {
        public CreateJobCategoryDto JobCategoryDto { get; set; }
    }
}
