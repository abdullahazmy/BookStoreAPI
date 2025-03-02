using BookStoreAPI.DTOs.OrderDTOs;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        UnitOfWorks _unit;
        public OrderController(UnitOfWorks unit)
        {
            _unit = unit;
        }
        [HttpPost]
        public IActionResult AddOrder(AddOrderDTO orderDTO)
        {
            // Create a new order
            var order = new Order()
            {
                CustomerId = orderDTO.CustomerId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                Status = "Create",
            };

            // Add the order to the repository
            _unit.OrderRepository.Add(order);
            _unit.Save();
            decimal totalPrice = 0;
            // Process each book in the order
            foreach (var item in orderDTO.Books)
            {
                var book = _unit.BookRepository.SelectById(item.BookId);
                totalPrice += book.Price * item.Quantity;
                if (book == null)
                {
                    return NotFound($"Book not found.");
                }

                if (book.Stock < item.Quantity)
                {
                    _unit.OrderRepository.Delete(order.Id);
                    return BadRequest($"The quantity of the book '{book.Title}' is not enough.");
                }

                var orderDetails = new OrderDetails
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = book.Price
                };

                // Add order details to the repository
                _unit.OrderDetailsRepository.Add(orderDetails);

                // Decrease the quantity of the book in the store
                book.Stock -= item.Quantity;
                _unit.BookRepository.Update(book);
            }
            order.TotalPrice = totalPrice;
            _unit.OrderRepository.Update(order);

            // Save all changes to the database
            _unit.Save();

            return Ok();
        }

    }
}
