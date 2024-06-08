using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class AuthorVM
    {
        public int Id { get; set; }

       // [Required(ErrorMessage = "Please Insert Name")]
       // [MaxLength(50, ErrorMessage = "The name field can't exceed 50 characteres")]
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatededAt { get; set; } = DateTime.Now;

    }
}
