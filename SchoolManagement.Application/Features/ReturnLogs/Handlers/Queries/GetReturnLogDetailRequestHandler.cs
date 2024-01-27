using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnLogs;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnLogs.Handlers.Queries
{
    public class GetReturnLogDetailRequestHandler : IRequestHandler<GetReturnLogDetailRequest, ReturnLogDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ReturnLog> _ReturnLogRepository;
        public GetReturnLogDetailRequestHandler(ISchoolManagementRepository<ReturnLog> ReturnLogRepository, IMapper mapper)
        {
            _ReturnLogRepository = ReturnLogRepository;
            _mapper = mapper;
        }
        public async Task<ReturnLogDto> Handle(GetReturnLogDetailRequest request, CancellationToken cancellationToken)
        {
            var ReturnLog = await _ReturnLogRepository.Get(request.ReturnLogId);
            return _mapper.Map<ReturnLogDto>(ReturnLog);
        }
    }
}
