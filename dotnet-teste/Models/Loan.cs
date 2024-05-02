
using Microsoft.AspNetCore.Identity;


namespace dotnet_teste.Models
{
    public class Loan
    {
        
        public int Id { get; set; }
        public int BookId { get; set; }
        public int? BookNSU { get; set; }
        public DateTime LoanTime { get; set; }        
        public string? UserId { get; set; }
        public DateTime ExpectedDelivery { get; set; }
        public DateTime? AppointedDeliveryDate { get; set; }
        public bool? IsDelivered { get; set; }        
        public virtual Books? Book { get; set; }
        public virtual IdentityUser User { get; set; }

    }
}
