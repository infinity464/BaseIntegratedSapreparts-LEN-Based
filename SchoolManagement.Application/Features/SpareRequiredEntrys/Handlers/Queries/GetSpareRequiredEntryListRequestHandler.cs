using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Queries
{
    public class GetSpareRequiredEntryListRequestHandler : IRequestHandler<GetSpareRequiredEntryListRequest, PagedResult<SpareRequiredEntryDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredEntry> _SpareRequiredEntryRepository;

        private readonly IMapper _mapper;

        public GetSpareRequiredEntryListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredEntry> SpareRequiredEntryRepository, IMapper mapper)
        {
            _SpareRequiredEntryRepository = SpareRequiredEntryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SpareRequiredEntryDto>> Handle(GetSpareRequiredEntryListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.SpareRequiredEntry> SpareRequiredEntrys = _SpareRequiredEntryRepository.FilterWithInclude(x => (x.Remarks.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "DepartmentName", "ItemDetail", "JobDescriptionEntryPms");
            var totalCount = SpareRequiredEntrys.Count();
            SpareRequiredEntrys = SpareRequiredEntrys.OrderByDescending(x => x.SpareRequiredEntryId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SpareRequiredEntryDtos = _mapper.Map<List<SpareRequiredEntryDto>>(SpareRequiredEntrys);
            var result = new PagedResult<SpareRequiredEntryDto>(SpareRequiredEntryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
