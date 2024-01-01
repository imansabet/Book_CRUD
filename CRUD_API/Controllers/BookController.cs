using AutoMapper;
using CRUD_API.Model;
using CRUD_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var books = await _bookRepository.GetBooksAsync(page, pageSize, search);
            var bookDTOs = _mapper.Map<IEnumerable<BookDTO>>(books);

            return Ok(bookDTOs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = _mapper.Map<BookDTO>(book);
            return Ok(bookDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            await _bookRepository.AddBookAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, bookDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (id != bookDTO.BookId)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookDTO);

            try
            {
                await _bookRepository.UpdateBookAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_bookRepository.GetBooksAsync(1, 1, "").Result.Any(e => e.BookId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteBookAsync(id);

            return NoContent();
        }

    }
}