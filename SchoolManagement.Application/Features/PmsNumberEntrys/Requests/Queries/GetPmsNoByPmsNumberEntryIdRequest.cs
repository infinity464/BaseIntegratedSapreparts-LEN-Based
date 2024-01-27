using MediatR;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text; 

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetPmsNoByPmsNumberEntryIdRequest : IRequest<List<SelectedModel>>
    {
        public int PmsNumberEntryId { get; set; }    
    }
} 
