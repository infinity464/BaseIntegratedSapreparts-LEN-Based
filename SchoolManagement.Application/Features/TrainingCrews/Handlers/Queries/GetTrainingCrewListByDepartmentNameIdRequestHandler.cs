using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.TrainingCrew;
using SchoolManagement.Application.Features.TrainingCrews.Requests.Queries;

namespace SchoolManagement.Application.Features.TrainingCrews.Handlers.Queries
{
    public class GetTrainingCrewListByDepartmentNameIdRequestHandler : IRequestHandler<GetTrainingCrewListByDepartmentNameIdRequest, List<TrainingCrewDto>>
    {
        private readonly ISchoolManagementRepository<TrainingCrew> _TrainingCrewRepository;

        private readonly IMapper _mapper;
        public GetTrainingCrewListByDepartmentNameIdRequestHandler(ISchoolManagementRepository<TrainingCrew> TrainingCrewRepository, IMapper mapper)
        {
            _TrainingCrewRepository = TrainingCrewRepository;
            _mapper = mapper;
        }

        public async Task<List<TrainingCrewDto>> Handle(GetTrainingCrewListByDepartmentNameIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<TrainingCrew> TrainingCrews = _TrainingCrewRepository.FilterWithInclude(x => x.DepartmentNameId == request.DepartmentNameId , "Rank", "OfficersStatus", "DepartmentName");

            var TrainingCrewDtos = _mapper.Map<List<TrainingCrewDto>>(TrainingCrews);

            return TrainingCrewDtos;
        }

    }
}
