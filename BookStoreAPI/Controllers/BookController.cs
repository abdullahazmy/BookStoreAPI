using BookStoreAPI.DTOs.BookDTOs;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        UnitOfWorks _unit;
        public BookController(UnitOfWorks _unit)
        {
            this._unit = _unit;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all books", Description = "Get all books from the database")]
        [SwaggerResponse(200, "Books retrieved successfully", typeof(List<DisplayBookDTO>))]
        [SwaggerResponse(404, "No books found")]
        public IActionResult SelectAllBooks()
        {
            List<Book> books = _unit.BookRepository.SelectAll();
            List<DisplayBookDTO> booksDTO = new List<DisplayBookDTO>();
            foreach (var book in books)
            {
                DisplayBookDTO bookDTO = new DisplayBookDTO()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Stock = book.Stock,
                    PublishDate = book.PublishDate,
                    AuthorName = book._Author.FullName,
                    CatalogName = book._Catalog.Name
                };
                booksDTO.Add(bookDTO);
            }
            return Ok(booksDTO);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get book by id", Description = "Get a book by its id from the database")]
        [SwaggerResponse(200, "Book retrieved successfully", typeof(DisplayBookDTO))]
        [SwaggerResponse(404, "Book not found")]
        public IActionResult GetBookById(int id)
        {
            Book book = _unit.BookRepository.SelectById(id);
            if (book == null)
            {
                return NotFound();
            }
            DisplayBookDTO bookDTO = new DisplayBookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Stock = book.Stock,
                PublishDate = book.PublishDate,
                AuthorName = book._Author.FullName,
                CatalogName = book._Catalog.Name
            };
            return Ok(bookDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add a book", Description = "Add a book to the database")]
        [SwaggerResponse(201, "Book added successfully", typeof(AddBookDTO))]
        [SwaggerResponse(400, "Invalid book data")]
        public IActionResult AddBook(AddBookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Book book = new Book()
            {
                Title = bookDTO.Title,
                Price = bookDTO.Price,
                Stock = bookDTO.Stock,
                PublishDate = bookDTO.PublishDate,
                AuthorId = bookDTO.AuthorId,
                CatalogId = bookDTO.CatalogId
            };
            _unit.BookRepository.Add(book);
            _unit.Save();
            return CreatedAtAction("GetBookById", new { id = book.Id }, bookDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update a book", Description = "Update a book in the database")]
        [SwaggerResponse(204, "Book updated successfully")]
        [SwaggerResponse(400, "Invalid book data")]
        public IActionResult UpdateBook(int id, AddBookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Book book = _unit.BookRepository.SelectById(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = bookDTO.Title;
            book.Price = bookDTO.Price;
            book.Stock = bookDTO.Stock;
            book.PublishDate = bookDTO.PublishDate;
            book.AuthorId = bookDTO.AuthorId;
            book.CatalogId = bookDTO.CatalogId;
            _unit.BookRepository.Update(book);
            _unit.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete a book", Description = "Delete a book from the database")]
        [SwaggerResponse(204, "Book deleted successfully")]
        [SwaggerResponse(404, "Book not found")]
        public IActionResult DeleteBook(int id)
        {
            Book book = _unit.BookRepository.SelectById(id);
            if (book == null)
            {
                return NotFound();
            }
            _unit.BookRepository.Delete(id);
            _unit.Save();
            return NoContent();
        }
    }
}