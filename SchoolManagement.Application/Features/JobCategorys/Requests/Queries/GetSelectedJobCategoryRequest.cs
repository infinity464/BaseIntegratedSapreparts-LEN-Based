using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobCategorys.Requests.Queries
{
    public class GetSelectedJobCategoryRequest : IRequest<List<SelectedModel>>
    {
    }
} 
