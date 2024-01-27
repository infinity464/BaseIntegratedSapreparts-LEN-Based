using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ReturnStatus;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries
{
   public class GetReturnStatusListRequest : IRequest<PagedResult<ReturnStatusDto>>
    {
        public QueryParams QueryParams { get; set; }
    } 
}
