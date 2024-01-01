using AutoMapper;
using CRUD_API.Model;
using CRUD_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var authors = await _authorRepository.GetAuthorsAsync(page, pageSize, search);
            var authorDTOs = _mapper.Map<IEnumerable<AuthorDTO>>(authors);

            return Ok(authorDTOs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return Ok(authorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            await _authorRepository.AddAuthorAsync(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, authorDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (id != authorDTO.AuthorId)
            {
                return BadRequest();
            }

            var author = _mapper.Map<Author>(authorDTO);

            try
            {
                await _authorRepository.UpdateAuthorAsync(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_authorRepository.GetAuthorsAsync(1, 1, "").Result.Any(e => e.AuthorId == id))
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
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            await _authorRepository.DeleteAuthorAsync(id);

            return NoContent();
        }
    }

}