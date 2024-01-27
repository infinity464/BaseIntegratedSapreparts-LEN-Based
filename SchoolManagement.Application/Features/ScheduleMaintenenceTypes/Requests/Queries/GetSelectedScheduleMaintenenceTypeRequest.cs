using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries
{
    public class GetSelectedScheduleMaintenenceTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
