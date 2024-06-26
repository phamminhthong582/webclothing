using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Response.Account;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountRespone>> GetAccountById(int id)
        {
            return await _accountRepository.GetAccountById(id); 
        }
    }
}
