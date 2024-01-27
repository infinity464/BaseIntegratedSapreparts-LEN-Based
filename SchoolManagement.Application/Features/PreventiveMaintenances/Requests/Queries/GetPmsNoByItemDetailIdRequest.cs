using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPmsNoByItemDetailIdRequest : IRequest<List<SelectedModel>>
    {
        public int ItemDetailId { get; set; }
    }
} 
