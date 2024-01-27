using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class SurveyItemType
    {
        public SurveyItemType()
        {
            IssueRegisters = new HashSet<IssueRegister>();
            SurveyItems = new HashSet<SurveyItem>();
            Surveys = new HashSet<Survey>();
        }

        public int SurveyItemTypeId { get; set; }
        public string SurveyItemTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
