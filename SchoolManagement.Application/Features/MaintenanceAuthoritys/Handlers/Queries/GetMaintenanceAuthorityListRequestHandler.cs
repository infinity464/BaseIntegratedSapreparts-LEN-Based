using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Handlers.Queries
{
    public class GetMaintenanceAuthorityListRequestHandler : IRequestHandler<GetMaintenanceAuthorityListRequest, PagedResult<MaintenanceAuthorityDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.MaintenanceAuthority> _MaintenanceAuthorityRepository;

        private readonly IMapper _mapper;

        public GetMaintenanceAuthorityListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.MaintenanceAuthority> MaintenanceAuthorityRepository, IMapper mapper)
        {
            _MaintenanceAuthorityRepository = MaintenanceAuthorityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<MaintenanceAuthorityDto>> Handle(GetMaintenanceAuthorityListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.MaintenanceAuthority> MaintenanceAuthoritys = _MaintenanceAuthorityRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = MaintenanceAuthoritys.Count();
            MaintenanceAuthoritys = MaintenanceAuthoritys.OrderByDescending(x => x.MaintenanceAuthorityId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var MaintenanceAuthorityDtos = _mapper.Map<List<MaintenanceAuthorityDto>>(MaintenanceAuthoritys);
            var result = new PagedResult<MaintenanceAuthorityDto>(MaintenanceAuthorityDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
