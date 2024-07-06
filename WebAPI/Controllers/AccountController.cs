using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Pagination;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        IConfiguration _config;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
           
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<AccountRespone>>> GetAccount()
        {
          return await _accountRepository.GetAllAccountAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{pageIndex}")]
        public async Task<ActionResult<Pagination<Account>>> GetAccountPagination(int pageIndex = 0)
        {
            return await _accountRepository.GetAllAccountPagination(pageIndex);
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountRespone>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountAsync();
            return Ok(accounts);
        }
        [AllowAnonymous]
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
        [HttpPut("{Update}")]
        public async Task<ActionResult> UpdateAccount(int id, [FromForm] UpdateAccountRequest model)
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
        [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<RepoRespone<string>> Login([FromForm] LoginAccountRespone lg)
        {
            var result = await _accountRepository.Login(lg.Email , lg.Password);
            return result;
        }


        }
    }
