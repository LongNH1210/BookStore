using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
namespace BookStore.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Book Book { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
