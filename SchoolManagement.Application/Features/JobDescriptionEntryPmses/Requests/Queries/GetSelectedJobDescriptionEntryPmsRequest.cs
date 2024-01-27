using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetSelectedJobDescriptionEntryPmsRequest : IRequest<List<SelectedModel>>
    {
    }
} 
