using MediatR;
using SchoolManagement.Application.DTOs.ReturnLogs;

namespace SchoolManagement.Application.Features.ReturnLogs.Requests.Queries
{
    public class GetReturnLogDetailRequest : IRequest<ReturnLogDto>
    {
        public int ReturnLogId { get; set; }
    }
}
