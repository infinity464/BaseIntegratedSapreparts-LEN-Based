using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetSelectedDemandNoByDemandIdRequest : IRequest<List<SelectedModel>>
    {
        public int DemandId { get; set; }
    }
}   
        