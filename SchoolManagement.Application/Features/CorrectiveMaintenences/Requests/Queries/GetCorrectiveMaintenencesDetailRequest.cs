using MediatR;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries
{
    public class GetCorrectiveMaintenenceDetailRequest : IRequest<CorrectiveMaintenenceDto>
    {
        public int CorrectiveMaintenenceId { get; set; }
    }
}
