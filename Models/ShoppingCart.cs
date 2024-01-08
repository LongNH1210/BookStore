using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
namespace BookStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<CartDetail> CartDetails { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
