using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class DepartmentName : BaseDomainEntity
    {
        //public DepartmentName()
        //{
        //    SurveyItems = new HashSet<SurveyItem>();
        //}
        public int DepartmentNameId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public bool IsActive { get; set; }
        //public virtual ICollection<SurveyItem> SurveyItems { get; set; }

    }
}
