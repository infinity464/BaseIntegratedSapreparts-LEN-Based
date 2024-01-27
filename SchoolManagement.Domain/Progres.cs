using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class Progres : BaseDomainEntity
    { 
        public Progres()
        {
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
        }
        public int ProgresId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
    }
}
