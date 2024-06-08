using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class AuthorFormVM
    {
        [Remote("CheckName",null, "Author name already exists")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Insert Name")]
        [MaxLength(50, ErrorMessage = "The name field can't exceed 50 characteres")]
        public string Name { get; set; } = null!;
       
    }
}
