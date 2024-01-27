using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries
{
    public class GetSelectedStatusOfJobRequest : IRequest<List<SelectedModel>>
    {
    }
} 
