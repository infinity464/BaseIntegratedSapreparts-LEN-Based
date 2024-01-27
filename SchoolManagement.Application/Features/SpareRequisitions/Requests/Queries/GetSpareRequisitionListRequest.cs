using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SpareRequisition;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries
{
    public class GetSpareRequisitionListRequest : IRequest<PagedResult<SpareRequisitionDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
