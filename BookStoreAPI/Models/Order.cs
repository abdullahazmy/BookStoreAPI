using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateOnly OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        [ForeignKey("_Customer")]
        public string CustomerId { get; set; } //???
        public virtual Customer _Customer { get; set; }

        public virtual List<OrderDetails> _OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
