using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Handlers.Queries
{
    public class GetScheduleMaintenenceTypeListRequestHandler : IRequestHandler<GetScheduleMaintenenceTypeListRequest, PagedResult<ScheduleMaintenenceTypeDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenenceType> _ScheduleMaintenenceTypeRepository;

        private readonly IMapper _mapper;

        public GetScheduleMaintenenceTypeListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenenceType> ScheduleMaintenenceTypeRepository, IMapper mapper)
        {
            _ScheduleMaintenenceTypeRepository = ScheduleMaintenenceTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ScheduleMaintenenceTypeDto>> Handle(GetScheduleMaintenenceTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.ScheduleMaintenenceType> ScheduleMaintenenceTypes = _ScheduleMaintenenceTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ScheduleMaintenenceTypes.Count();
            ScheduleMaintenenceTypes = ScheduleMaintenenceTypes.OrderByDescending(x => x.ScheduleMaintenenceTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ScheduleMaintenenceTypeDtos = _mapper.Map<List<ScheduleMaintenenceTypeDto>>(ScheduleMaintenenceTypes);
            var result = new PagedResult<ScheduleMaintenenceTypeDto>(ScheduleMaintenenceTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
