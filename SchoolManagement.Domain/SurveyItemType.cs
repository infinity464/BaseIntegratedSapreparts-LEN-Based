using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class SurveyItemType : BaseDomainEntity
    {
        public SurveyItemType()
        {
            SurveyItems = new HashSet<SurveyItem>();
            IssueRegisters = new HashSet<IssueRegister>();
            Surveys = new HashSet<Survey>();
        }

        public int SurveyItemTypeId { get; set; }
        public string? SurveyItemTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
