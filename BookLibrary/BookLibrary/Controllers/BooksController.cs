using BookLibrary.Data.Dtos;
using BookLibrary.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookForView>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookForView>> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<BookForView>>> GetBooksByCategory(string categoryName)
        {
            var books = await _bookService.GetBooksByCategoryAsync(categoryName);
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<BookForView>> AddBook([FromForm] BookReadDto bookDto)
        {
            var book = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadBook(Guid id)
        {
            try
            {
                var fileBytes = await _bookService.DownloadBookAsync(id);
                var book = await _bookService.GetBookByIdAsync(id);
                return File(fileBytes, "application/octet-stream", $"{book.Title}.pdf");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
