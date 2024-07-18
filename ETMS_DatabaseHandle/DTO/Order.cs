using System;

namespace ETMS_DatabaseHandle.DTO
{
    internal class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime PaymentDate { get; set; }
        public long TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
