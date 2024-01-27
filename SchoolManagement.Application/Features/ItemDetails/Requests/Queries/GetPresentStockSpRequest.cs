using MediatR;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetPresentStockSpRequest : IRequest<object>
    {
        public int DepartmentId { get; set; }
    }
}
