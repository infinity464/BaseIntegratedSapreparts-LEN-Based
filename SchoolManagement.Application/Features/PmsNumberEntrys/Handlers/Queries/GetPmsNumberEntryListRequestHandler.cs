using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetPmsNumberEntryListRequestHandler : IRequestHandler<GetPmsNumberEntryListRequest, PagedResult<PmsNumberEntryDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.PmsNumberEntry> _PmsNumberEntryRepository;

        private readonly IMapper _mapper;

        public GetPmsNumberEntryListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.PmsNumberEntry> PmsNumberEntryRepository, IMapper mapper)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PmsNumberEntryDto>> Handle(GetPmsNumberEntryListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.PmsNumberEntry> PmsNumberEntrys = _PmsNumberEntryRepository.FilterWithInclude(x => (x.PmsNo.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "DepartmentName", "ItemDetail");
            var totalCount = PmsNumberEntrys.Count();
            PmsNumberEntrys = PmsNumberEntrys.OrderByDescending(x => x.PmsNumberEntryId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var PmsNumberEntryDtos = _mapper.Map<List<PmsNumberEntryDto>>(PmsNumberEntrys);
            var result = new PagedResult<PmsNumberEntryDto>(PmsNumberEntryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
