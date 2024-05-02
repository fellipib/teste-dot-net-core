using dotnet_teste.Models;
using dotnet_teste.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public LoanController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var loans = await _context.Loans.ToListAsync();
                return Ok(loans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("bynsu")]
        public async Task<ActionResult> LoansByBookNSU(int NSU)
        {
            try
            {
                var books = await _context.Books.FirstOrDefaultAsync(x => x.NSU == NSU);
                if (books == null)
                    return NotFound("Nenhum com este NSU");


                var loans = await _context.Loans.Where(x => x.BookNSU == NSU).ToListAsync();
                if (loans == null)
                    return NotFound("Nenhum emprestimo para este NSU");
                return Ok(loans);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Loan loan)
        {
            try
            {
                if (loan == null || loan.BookId == null)
                    return BadRequest("Verifique os dados do emprestimo");
                var book = await _context.Books.FirstOrDefaultAsync(x => x.NSU == loan.BookNSU);
                if (book == null)
                    return BadRequest("Esse livro não existe");
                var user = await GetUser();
                var today = DateTime.Now;
                loan.BookId = book.Id;
                loan.LoanTime = today;
                loan.ExpectedDelivery = today.AddDays(10);
                loan.UserId = user.Id;
                loan.IsDelivered = true;
                await _context.Loans.AddAsync(loan);
                await _context.SaveChangesAsync();

                return Ok(loan);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Delivery([FromBody] Loan loan)
        {
            try
            {
                var user = await GetUser();
                var oldLoan = await _context.Loans.FirstOrDefaultAsync(x => x.UserId == user.Id && x.BookNSU == loan.BookNSU);
                if (oldLoan == null)
                    return NotFound("Nenhum livro com este NSU emprestado para este usuário");

                var updatedLoan = oldLoan;
                updatedLoan.AppointedDeliveryDate = DateTime.Now;
                updatedLoan.IsDelivered = true;

                _context.Entry(oldLoan).CurrentValues.SetValues(updatedLoan);
                await _context.SaveChangesAsync();


                return Ok("Livro devolvido");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] Loan loan)
        {
            try
            {
                if (loan == null)
                    return BadRequest();

                var oldLoan = _context.Loans.FirstOrDefault(x => x.Id == loan.Id);
                if (oldLoan != null)
                {
                    var isDelete = _context.Loans.Remove(oldLoan);
                    await _context.SaveChangesAsync();
                    return Ok("Emprestimo apagado com sucesso");
                }
                return BadRequest("Erro");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("lateDelivery")]
        public async Task<ActionResult> LateDeliery()
        {
            try
            {
                var loans = await _context.Loans.Where(x => x.IsDelivered == false && x.ExpectedDelivery < DateTime.Today).Include(x => x.User).ToListAsync();


                return Ok(loans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private async Task<IdentityUser> GetUser()
        {
                var user = await _userManager.FindByNameAsync(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            return user;
        }



    }

}
