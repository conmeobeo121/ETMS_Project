using System;

namespace ETMS_DatabaseHandle.DTO
{
    internal class TicketType
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int Price { get; set; }
        public bool HasSeat { get; set; }
        public DateTime StartSell { get; set; }
        public DateTime EndSell { get; set; }
        public int EventID { get; set; }
    }
}
