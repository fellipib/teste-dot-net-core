using dotnet_teste.Models;
using dotnet_teste.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("bynsu")]
        public async Task<ActionResult> Get(int NSU)
        {
            try
            {
                var books = await _context.Books.FirstOrDefaultAsync(x => x.NSU == NSU);
                return Ok(books);

            }
            catch(Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Books book) 
        { 
        
            try
            {
                if (book == null)
                    return BadRequest();

                if (book.NSU == null)
                    return BadRequest("Um NSU é necessário");

                if (book.Title.IsNullOrEmpty())
                    return BadRequest("Um titulo é necessário");
                if (book.Category.IsNullOrEmpty())
                    return BadRequest("Uma categoria é necessária");

                var alreadyExists = await _context.Books.FindAsync(book.NSU);
                if (alreadyExists != null)
                    return BadRequest("Este NSU ja esta cadastrado");

                var newBook = _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Books book)
        {
            try
            {
                if (book == null)
                    return BadRequest();

                if (book.NSU == null)
                    return BadRequest("Um NSU é necessário");
                if (book.Title.IsNullOrEmpty())
                    return BadRequest("Um titulo é necessário");
                if (book.Category.IsNullOrEmpty())
                    return BadRequest("Uma categoria é necessária");

                var oldBook = await _context.Books.FirstOrDefaultAsync(x => x.NSU == book.NSU);

                if (oldBook == null)
                    return NotFound();


                _context.Entry(oldBook).CurrentValues.SetValues(book);

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] Books book)
        {
            try
            {
                if (book == null)
                    return BadRequest();

                if (book.NSU == null)
                    return BadRequest("Um NSU é necessário");

                var oldBook = _context.Books.FirstOrDefault(x => x.NSU == book.NSU);
                if (oldBook != null)
                {
                    var isDelete = _context.Books.Remove(oldBook);
                    await _context.SaveChangesAsync();
                    return Ok("Livro apagado com sucesso");
                }
                return BadRequest("Erro");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("mostLoanedBooks")]
        public async Task<ActionResult> getMostLoanedBooks()
        {
            try
            {

                var query = await _context.Books.Include(x => x.Loans).ToListAsync();
                var res = query.Select(livro => new {livro, TotalImprestimos = livro.Loans.Count()}).OrderByDescending(x => x.TotalImprestimos);
                                                                                                                                                          
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
