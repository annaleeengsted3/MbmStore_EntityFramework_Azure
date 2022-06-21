using System.Collections.Generic;

namespace MbmStore.Models.ViewModels
{
    public class Order
    {
        public int OrderID { get; set; }
        //public ICollection<CartLine> Lines { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
