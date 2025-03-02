using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.BookDTOs
{
    public class AddBookDTO
    {
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public int AuthorId { get; set; }
        public int? CatalogId { get; set; }
    }
}
