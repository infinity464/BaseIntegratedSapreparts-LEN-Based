using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Group;
using SchoolManagement.Application.Features.Groups.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Groups.Handlers.Queries
{
    public class GetGroupDetailRequestHandler : IRequestHandler<GetGroupDetailRequest, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Group> _GroupRepository;
        public GetGroupDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Group> GroupRepository, IMapper mapper)
        {
            _GroupRepository = GroupRepository;
            _mapper = mapper;
        }
        public async Task<GroupDto> Handle(GetGroupDetailRequest request, CancellationToken cancellationToken)
        {
            var Group = await _GroupRepository.Get(request.GroupId);
            return _mapper.Map<GroupDto>(Group);
        }
    }
}
