using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Handlers.Queries
{
    public class GetEquipmentPriorityListRequestHandler : IRequestHandler<GetEquipmentPriorityListRequest, PagedResult<EquipmentPriorityDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.EquipmentPriority> _EquipmentPriorityRepository;

        private readonly IMapper _mapper;

        public GetEquipmentPriorityListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.EquipmentPriority> EquipmentPriorityRepository, IMapper mapper)
        {
            _EquipmentPriorityRepository = EquipmentPriorityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EquipmentPriorityDto>> Handle(GetEquipmentPriorityListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.EquipmentPriority> EquipmentPrioritys = _EquipmentPriorityRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = EquipmentPrioritys.Count();
            EquipmentPrioritys = EquipmentPrioritys.OrderByDescending(x => x.EquipmentPriorityId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var EquipmentPriorityDtos = _mapper.Map<List<EquipmentPriorityDto>>(EquipmentPrioritys);
            var result = new PagedResult<EquipmentPriorityDto>(EquipmentPriorityDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
