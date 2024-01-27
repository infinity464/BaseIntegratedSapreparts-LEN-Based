using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetSelectedItemNameFromPmsNumberEntryRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }

    }
}  
  