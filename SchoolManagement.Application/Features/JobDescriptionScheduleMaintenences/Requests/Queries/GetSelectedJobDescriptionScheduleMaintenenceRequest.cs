using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries
{
    public class GetSelectedJobDescriptionScheduleMaintenenceRequest : IRequest<List<SelectedModel>>
    {
    }
} 
 