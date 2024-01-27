using MediatR;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands
{
    public class UpdateJobCategoryMaintenenceCommand : IRequest<Unit>
    { 
        public JobCategoryMaintenenceDto JobCategoryMaintenenceDto { get; set; }
    }
}
 