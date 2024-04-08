using BooStoreApp.Api.DataContext;
using BooStoreApp.Api.Models;
using BooStoreApp.Api.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly Context _context;
        private readonly ResponseDto _responseDto;

        public AuthorController(Context context,ResponseDto responseDto)
        {
            _context = context;
            _responseDto=responseDto;
        }
        [HttpGet]
        public ResponseDto GetAuthors()
        {
            try
            {
                var authors = _context.Author.ToList();
                _responseDto.Result = authors;

            }catch (Exception ex)
            {
                _responseDto.IsSuccess= false;
                _responseDto.Message= ex.Message;
            }
            return _responseDto;
        }
        [HttpGet("{id}")]
        public ResponseDto GetAuthorById(int id)
        {
            var authorFound = _context.Author.FindAsync(id).Result;
            if(authorFound == null)
            {
                _responseDto.IsSuccess= false;
                _responseDto.Message = "No Author found with this Id";
            }
            else
            {
                _responseDto.Result = authorFound;
            }
            return _responseDto;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id,Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            _context.Entry(author).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException ex)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAUthor",new {id=author.Id},author);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = _context.Author.FindAsync(id).Result;
            if(author == null)
            {
                return NotFound(id);
            }
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e=>e.Id == id);
        }
    }
}
