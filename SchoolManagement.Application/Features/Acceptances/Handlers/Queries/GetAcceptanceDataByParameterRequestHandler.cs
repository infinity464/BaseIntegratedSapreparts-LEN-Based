using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.Acceptances.Requests.Queries;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Application.DTOs.Acceptances;

namespace SchoolManagement.Application.Features.Acceptances.Handlers.Queries
{
    public class GetAcceptanceDataByParameterRequestHandler : IRequestHandler<GetAcceptanceDataByParameterRequest, List<AcceptanceDto>>
    {
        private readonly ISchoolManagementRepository<Acceptance> _AcceptanceRepository;

        private readonly IMapper _mapper;
        public GetAcceptanceDataByParameterRequestHandler(ISchoolManagementRepository<Acceptance> AcceptanceRepository, IMapper mapper)
        {
            _AcceptanceRepository = AcceptanceRepository;
            _mapper = mapper;
        }
         
        public async Task<List<AcceptanceDto>> Handle(GetAcceptanceDataByParameterRequest request, CancellationToken cancellationToken)
        {
            var Acceptances = new List<Acceptance>();
            try
            {
                 Acceptances = _AcceptanceRepository.FilterWithInclude(x => x.IsActive==true, "ItemDetail").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var AcceptanceDtos = _mapper.Map<List<AcceptanceDto>>(Acceptances);

            return AcceptanceDtos;
        }

    }
}
