using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetSelectedPmsNoByItemDetailIdRequest : IRequest<List<SelectedModel>>
    {
        public int ItemDetailId { get; set; }
    }
} 
