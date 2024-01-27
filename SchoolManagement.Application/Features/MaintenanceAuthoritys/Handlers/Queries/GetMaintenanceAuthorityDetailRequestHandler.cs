using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Handlers.Queries
{
    public class GetMaintenanceAuthorityDetailRequestHandler : IRequestHandler<GetMaintenanceAuthorityDetailRequest, MaintenanceAuthorityDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.MaintenanceAuthority> _MaintenanceAuthorityRepository;
        public GetMaintenanceAuthorityDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.MaintenanceAuthority> MaintenanceAuthorityRepository, IMapper mapper)
        {
            _MaintenanceAuthorityRepository = MaintenanceAuthorityRepository;
            _mapper = mapper;
        }
        public async Task<MaintenanceAuthorityDto> Handle(GetMaintenanceAuthorityDetailRequest request, CancellationToken cancellationToken)
        {
            var MaintenanceAuthority = await _MaintenanceAuthorityRepository.Get(request.MaintenanceAuthorityId);
            return _mapper.Map<MaintenanceAuthorityDto>(MaintenanceAuthority);
        }
    }
}
