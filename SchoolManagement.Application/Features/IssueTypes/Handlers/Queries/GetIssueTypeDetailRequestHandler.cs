using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueType;
using SchoolManagement.Application.Features.IssueTypes.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.IssueTypes.Handlers.Queries
{
    public class GetIssueTypeDetailRequestHandler : IRequestHandler<GetIssueTypeDetailRequest, IssueTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.IssueType> _IssueTypeRepository;
        public GetIssueTypeDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.IssueType> IssueTypeRepository, IMapper mapper)
        {
            _IssueTypeRepository = IssueTypeRepository;
            _mapper = mapper;
        }
        public async Task<IssueTypeDto> Handle(GetIssueTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var IssueType = await _IssueTypeRepository.Get(request.IssueTypeId);
            return _mapper.Map<IssueTypeDto>(IssueType);
        }
    }
}
