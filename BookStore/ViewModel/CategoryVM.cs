using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookStore.ViewModel
{
    public class CategoryVM
    {

        public int Id { get; set; }

       // [Remote("CheckName",null,ErrorMessage ="")]
        [Required(ErrorMessage="Please Insert Name")]
        [MaxLength(30,ErrorMessage ="length must less than 30")]
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
