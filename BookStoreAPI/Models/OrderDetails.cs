using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class OrderDetails
    {
        [ForeignKey("_Order")]
        public int OrderId { get; set; }
        [ForeignKey("_Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public virtual Order _Order { get; set; }
        public virtual Book _Book { get; set; }

    }
}
