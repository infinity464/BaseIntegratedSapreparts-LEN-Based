using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Demands = new HashSet<Demand>();
        }

        public int SurveyId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? SurveyAuthorityId { get; set; }
        public int? SurveyItemTypeId { get; set; }
        public int? SurveyQty { get; set; }
        public int? IssueQty { get; set; }
        public string SurveyNumber { get; set; }
        public DateTime? SurveyDate { get; set; }
        public string ByWhom { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public int? ItemCategoryId { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual IssueRegister IssueRegister { get; set; }
        public virtual SurveyAuthority SurveyAuthority { get; set; }
        public virtual SurveyItemType SurveyItemType { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
    }
}
