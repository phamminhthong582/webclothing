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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;       
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
           
        }
        [HttpGet]
        public async Task<ActionResult<List<AccountRespone>>> GetAllAccounts()
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
        public async Task<ActionResult<AccountRespone>> CreateAccount(CreateAccountRequest model)
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
