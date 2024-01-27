using MediatR;
using SchoolManagement.Application.DTOs.TrainingCrew;

namespace SchoolManagement.Application.Features.TrainingCrews.Requests.Queries
{
    public class GetTrainingCrewListByDepartmentNameIdRequest : IRequest<List<TrainingCrewDto>>
    {
        public int DepartmentNameId { get; set; }

    } 
}

 