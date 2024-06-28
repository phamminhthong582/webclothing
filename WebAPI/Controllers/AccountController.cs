using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly WebCoustemClothingContext _context;
        public AccountController(IAccountRepository accountRepository, WebCoustemClothingContext context)
        {
            _accountRepository = accountRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountAsync();
            return Ok(accounts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountRespone>> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAccountById(id);
            return Ok(account);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAccount(CreateAccountRequest model)
        {
            try
            {
                var createdAccount = await _accountRepository.CreateAccount(model);
                return Ok(createdAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccount(int id, UpdateAccountRequest model)
        {
                if (id != model.AccountId)
                {
                    return BadRequest("ID không khớp");
                }
                try
                {
                    var updatedAccount = await _accountRepository.UpdateAccount(model);
                    return Ok(updatedAccount);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
        }
        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAccountAsync(int id)
            {
                try
                {
                    await _accountRepository.DeleteAccountAsync(id);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
    }
