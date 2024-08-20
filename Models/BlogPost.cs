using System.ComponentModel.DataAnnotations;

namespace BlogManagementApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "DateCreated is required.")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Text is required.")]
        [StringLength(1000, ErrorMessage = "Text cannot be longer than 1000 characters.")]
        public string Text { get; set; }
    }
}