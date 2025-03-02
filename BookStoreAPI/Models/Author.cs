using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        public string Bio { get; set; }
        public int Age { get; set; }
        public int NumberOfBooks { get; set; }

        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
