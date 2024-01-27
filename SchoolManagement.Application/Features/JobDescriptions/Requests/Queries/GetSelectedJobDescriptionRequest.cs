using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptions.Requests.Queries
{
    public class GetSelectedJobDescriptionRequest : IRequest<List<SelectedModel>>
    {
    }
} 
