using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PublishDate { get; set; }

        [ForeignKey("_Catalog")]
        public int? CatalogId { get; set; }
        public virtual Catalog? _Catalog { get; set; }

        [ForeignKey("_Author")]
        public int AuthorId { get; set; }
        public virtual Author _Author { get; set; }

        public virtual List<OrderDetails> _OrderDetails { get; set; } = new List<OrderDetails>();

    }
}
