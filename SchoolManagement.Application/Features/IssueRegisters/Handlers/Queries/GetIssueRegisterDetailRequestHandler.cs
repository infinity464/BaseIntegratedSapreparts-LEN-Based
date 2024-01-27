using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetIssueRegisterDetailRequestHandler : IRequestHandler<GetIssueRegisterDetailRequest, IssueRegisterDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.IssueRegister> _IssueRegisterRepository;
        public GetIssueRegisterDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.IssueRegister> IssueRegisterRepository, IMapper mapper)
        {
            _IssueRegisterRepository = IssueRegisterRepository;
            _mapper = mapper;
        }
        public async Task<IssueRegisterDto> Handle(GetIssueRegisterDetailRequest request, CancellationToken cancellationToken)
        {
            var IssueRegister = _IssueRegisterRepository.FinedOneInclude(x=>x.IssueRegisterId == request.IssueRegisterId, "ItemDetail", "ToolsLocation");
            return _mapper.Map<IssueRegisterDto>(IssueRegister);
        }
    }
}
