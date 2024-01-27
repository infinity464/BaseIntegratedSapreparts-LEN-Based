using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries
{
    public class GetSelectedMaintenanceAuthorityRequest : IRequest<List<SelectedModel>>
    {
    }
} 
