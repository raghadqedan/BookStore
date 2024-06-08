using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Authors{ get; set;}

        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatededAt { get; set; } = DateTime.Now;

        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();
    }

}

