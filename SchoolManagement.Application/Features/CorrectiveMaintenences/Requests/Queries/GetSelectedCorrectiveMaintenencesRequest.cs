using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries
{
    public class GetSelectedCorrectiveMaintenenceRequest : IRequest<List<SelectedModel>>
    {
    }
} 
