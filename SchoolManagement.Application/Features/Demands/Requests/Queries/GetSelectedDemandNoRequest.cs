using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetSelectedDemandNoRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; } 
        public int DepartmentId { get; set; } 
    }
}   
     