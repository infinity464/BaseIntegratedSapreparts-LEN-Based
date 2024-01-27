using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries
{
    public class GetSelectedJobCategoryMaintenenceRequest : IRequest<List<SelectedModel>>
    {
    }
} 
