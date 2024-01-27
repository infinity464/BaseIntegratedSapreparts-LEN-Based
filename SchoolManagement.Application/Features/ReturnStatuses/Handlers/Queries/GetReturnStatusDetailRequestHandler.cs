using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnStatus;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnStatuss.Handlers.Queries
{
    public class GetReturnStatusDetailRequestHandler : IRequestHandler<GetReturnStatusDetailRequest, ReturnStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ReturnStatus> _ReturnStatusRepository;
        public GetReturnStatusDetailRequestHandler(ISchoolManagementRepository<ReturnStatus> ReturnStatusRepository, IMapper mapper)
        {
            _ReturnStatusRepository = ReturnStatusRepository;
            _mapper = mapper;
        }
        public async Task<ReturnStatusDto> Handle(GetReturnStatusDetailRequest request, CancellationToken cancellationToken)
        {
            var ReturnStatus = await _ReturnStatusRepository.Get(request.ReturnStatusId);
            return _mapper.Map<ReturnStatusDto>(ReturnStatus);
        }
    }
}
