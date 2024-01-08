using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class CategoryView
    {
        public string? CategoryName { get; set; }
        public string? SearchString { get; set; }
        [NotMapped]
        public List<Book>? Books { get; set; }
        [NotMapped]
        public SelectList? Categories { get; set; }
    }
}
