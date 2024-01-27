using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic; 
using System.Text;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries  
{
    public class GetSelectedSchoolNameByFourthLevelRequest : IRequest<List<SelectedModel>> 
    {
        public int FourthLevel { get; set; }
    }
} 
   