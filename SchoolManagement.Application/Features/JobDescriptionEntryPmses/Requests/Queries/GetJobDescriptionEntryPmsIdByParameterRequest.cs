using MediatR;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text; 

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJobDescriptionEntryPmsIdByParameterRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }    
        public int DepartmentNameId { get; set; }    
        public int MaintenanceTypeId { get; set; }     
        public int MaintenanceCategoryId { get; set; }    
        public int ItemDetailId { get; set; }     
    }
} 
 