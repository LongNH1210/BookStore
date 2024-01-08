using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}
