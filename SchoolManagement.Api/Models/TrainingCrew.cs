using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class TrainingCrew
    {
        public TrainingCrew()
        {
            IssueRegisters = new HashSet<IssueRegister>();
        }

        public int TrainingCrewId { get; set; }
        public int? CourseId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? OfficersStatusId { get; set; }
        public int? RankId { get; set; }
        public string Pno { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public string Duties { get; set; }
        public string AviationCategory { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Course Course { get; set; }
        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual OfficersStatus OfficersStatus { get; set; }
        public virtual Rank Rank { get; set; }
        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
    }
}
