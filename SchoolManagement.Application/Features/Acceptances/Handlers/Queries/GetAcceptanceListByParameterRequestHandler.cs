using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.Acceptances;
using SchoolManagement.Application.Features.Acceptances.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.DTOs.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.Features.Acceptances.Handlers.Queries
{ 
    public class GetAcceptanceListByParameterRequestHandler : IRequestHandler<GetAcceptanceListByParameterRequest, PagedResult<AcceptanceDto>>
    {
        private readonly ISchoolManagementRepository<Acceptance> _AcceptanceRepository;

        private readonly IMapper _mapper;
        public GetAcceptanceListByParameterRequestHandler(ISchoolManagementRepository<Acceptance> AcceptanceRepository, IMapper mapper)
        {
            _AcceptanceRepository = AcceptanceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AcceptanceDto>> Handle(GetAcceptanceListByParameterRequest request, CancellationToken cancellationToken)
        {
           // (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText))
            IQueryable<Acceptance> Acceptances = _AcceptanceRepository.FilterWithInclude(x =>String.IsNullOrEmpty(request.QueryParams.SearchText), "Demand", "ItemInspection", "Opinion");
            var totalCount = Acceptances.Count();
            Acceptances = Acceptances.OrderByDescending(x => x.AcceptanceId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize).Where(x=>x.UnitId == request.UnitId && x.DepartmentNameId ==request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId);

            var AcceptanceDtos = _mapper.Map<List<AcceptanceDto>>(Acceptances);
            var result = new PagedResult<AcceptanceDto>(AcceptanceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }

    }
}
