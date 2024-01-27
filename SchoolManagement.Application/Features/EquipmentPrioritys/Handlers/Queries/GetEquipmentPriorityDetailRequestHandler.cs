using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Handlers.Queries
{
    public class GetEquipmentPriorityDetailRequestHandler : IRequestHandler<GetEquipmentPriorityDetailRequest, EquipmentPriorityDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.EquipmentPriority> _EquipmentPriorityRepository;
        public GetEquipmentPriorityDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.EquipmentPriority> EquipmentPriorityRepository, IMapper mapper)
        {
            _EquipmentPriorityRepository = EquipmentPriorityRepository;
            _mapper = mapper;
        }
        public async Task<EquipmentPriorityDto> Handle(GetEquipmentPriorityDetailRequest request, CancellationToken cancellationToken)
        {
            var EquipmentPriority = await _EquipmentPriorityRepository.Get(request.EquipmentPriorityId);
            return _mapper.Map<EquipmentPriorityDto>(EquipmentPriority);
        }
    }
}
