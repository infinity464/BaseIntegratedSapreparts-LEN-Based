using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.AirCraftFlying;
using SchoolManagement.Application.Features.AirCraftFlyings.Requests.Queries;

namespace SchoolManagement.Application.Features.AirCraftFlyings.Handlers.Queries
{
    public class GetAirCraftFlyingListByDepartmentNameIdRequestHandler : IRequestHandler<GetAirCraftFlyingListByDepartmentNameIdRequest, List<AirCraftFlyingDto>>
    {
        private readonly ISchoolManagementRepository<AirCraftFlying> _AirCraftFlyingRepository;

        private readonly IMapper _mapper;
        public GetAirCraftFlyingListByDepartmentNameIdRequestHandler(ISchoolManagementRepository<AirCraftFlying> AirCraftFlyingRepository, IMapper mapper)
        {
            _AirCraftFlyingRepository = AirCraftFlyingRepository;
            _mapper = mapper;
        }

        public async Task<List<AirCraftFlyingDto>> Handle(GetAirCraftFlyingListByDepartmentNameIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<AirCraftFlying> AirCraftFlyings = _AirCraftFlyingRepository.FilterWithInclude(x => x.AirCraftNameId == request.AirCraftNameId && x.DepartmentNameId == request.DepartmentNameId , "DepartmentName", "AirCraftName");
            var totalCount = AirCraftFlyings.Count();
            AirCraftFlyings = AirCraftFlyings.OrderByDescending(x => x.Date);
            var AirCraftFlyingDtos = _mapper.Map<List<AirCraftFlyingDto>>(AirCraftFlyings);

            return AirCraftFlyingDtos;
        }

    }
}
