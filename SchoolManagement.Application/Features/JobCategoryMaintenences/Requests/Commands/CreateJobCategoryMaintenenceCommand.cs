using MediatR;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands
{
    public class CreateJobCategoryMaintenenceCommand : IRequest<BaseCommandResponse>
    {
        public CreateJobCategoryMaintenenceDto JobCategoryMaintenenceDto { get; set; }
    }
}
