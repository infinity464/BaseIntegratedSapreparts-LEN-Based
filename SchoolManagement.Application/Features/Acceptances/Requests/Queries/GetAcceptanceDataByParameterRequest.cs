using MediatR;
using SchoolManagement.Application.DTOs.Acceptances;
using SchoolManagement.Application.DTOs.ItemDetail;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetAcceptanceDataByParameterRequest : IRequest<List<AcceptanceDto>>
    {  
        public int DepartmentNameId { get; set; }
      //  public int SparesCategoryId { get; set; } 
    } 
}
 
   