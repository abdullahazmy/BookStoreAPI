namespace BookStoreAPI.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public string CustomerId { get; set; }
        //public decimal TotalPrice { get; set; }
        //public string Status { get; set; }
        public List<AddDetailsDTO> Books { get; set; } = new List<AddDetailsDTO>();
    }
}
