using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class Trade : BaseDomainEntity
    {
        public Trade()
        {
            ItemDetails = new HashSet<ItemDetail>();
            Demands = new HashSet<Demand>();
            CallibrationStates = new HashSet<CallibrationState>();
        }

        public int TradeId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<CallibrationState> CallibrationStates { get; set; }
    }
}
