using MediatR;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text; 

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJICByJobDescriptionEntryPmsIdRequest : IRequest<List<SelectedModel>>
    {
        public int JobDescriptionEntryPmsId { get; set; }    
    }
} 
