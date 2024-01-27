using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetSelectedPreventiveMaintenanceRequest : IRequest<List<SelectedModel>>
    {
    }
} 
