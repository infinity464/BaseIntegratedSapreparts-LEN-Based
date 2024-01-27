using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.EquipmentName;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;

namespace SchoolManagement.Application.Features.EquipmentNames.Handlers.Queries
{
    public class GetEquipmentNameListByDepartmentNameIdRequestHandler : IRequestHandler<GetEquipmentNameListByDepartmentNameIdRequest, List<EquipmentNameDto>>
    {
        private readonly ISchoolManagementRepository<EquipmentName> _EquipmentNameRepository;

        private readonly IMapper _mapper;
        public GetEquipmentNameListByDepartmentNameIdRequestHandler(ISchoolManagementRepository<EquipmentName> EquipmentNameRepository, IMapper mapper)
        {
            _EquipmentNameRepository = EquipmentNameRepository;
            _mapper = mapper;
        }

        public async Task<List<EquipmentNameDto>> Handle(GetEquipmentNameListByDepartmentNameIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EquipmentName> EquipmentNames = _EquipmentNameRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId ==  request.DepartmentNameId && x.ItemCategoryId ==request.ItemCategoryId, "ItemCategory");
            var totalCount = EquipmentNames.Count();
            EquipmentNames = EquipmentNames.OrderByDescending(x => x.EquipmentNameId);
            var EquipmentNameDtos = _mapper.Map<List<EquipmentNameDto>>(EquipmentNames);

            return EquipmentNameDtos;
        }

    }
}
