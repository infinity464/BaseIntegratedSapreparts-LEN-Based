using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries
{
    public class GetSelectedScheduleMaintenanceRequest : IRequest<List<SelectedModel>>
    {
    }
} 
