using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.Demands;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.DTOs.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetDemandListForSparesByDepartmentNameIdRequestHandler : IRequestHandler<GetDemandListForSparesByDepartmentNameIdRequest, PagedResult<DemandDto>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;

        private readonly IMapper _mapper;
        public GetDemandListForSparesByDepartmentNameIdRequestHandler(ISchoolManagementRepository<Demand> DemandRepository, IMapper mapper)
        {
            _DemandRepository = DemandRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<DemandDto>> Handle(GetDemandListForSparesByDepartmentNameIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude(x => (x.ItemDetail.PartNo.Contains(request.QueryParams.SearchText) || x.ItemDetail.NameOfItem.Contains(request.QueryParams.SearchText) || x.DemandNo.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "ItemCategory", "Authority", "DepartmentName", "Deno", "ItemDetail", "ConditionOfItem", "ItemStatus", "EquipmentName").Where(x => x.DepartmentNameId == request.DepartmentNameId && x.UnitId == request.UnitId && x.ItemCategoryId == request.ItemCategoryId);
            var totalCount = Demands.Count();
            Demands = Demands.OrderByDescending(x => x.DemandId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var DemandDtos = _mapper.Map<List<DemandDto>>(Demands);
            var result = new PagedResult<DemandDto>(DemandDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }

    }
}
