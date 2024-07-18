using System;

namespace ETMS_DatabaseHandle.DTO
{
    internal class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStartDate { get; set; } = DateTime.Now;
        public DateTime EventEndDate { get; set; }
        public int VenueID { get; set; }
        public int TypeID { get; set; }
    }
}
