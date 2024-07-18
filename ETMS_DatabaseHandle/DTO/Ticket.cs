namespace ETMS_DatabaseHandle.DTO
{
    internal class Ticket
    {
        public int TicketID { get; set; }
        public int OrderID { get; set; }
        public int TypeID { get; set; }
        public string TicketCode { get; set; }
        public int? SeatNumber { get; set; }
    }
}
