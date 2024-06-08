using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Author
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatededAt { get; set; } = DateTime.Now;
    }
}
