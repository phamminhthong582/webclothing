using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface IAccountRepository
    {
        Task<List<Account>>GetAllAccountAsync();
        Task<AccountRespone>GetAccountById(int id);
        Task<Account>UpdateAccount(UpdateAccountRequest request);
        Task DeleteAccountAsync(int id);
        Task<Account> GetAccountByEmail(string email);
        Task<Account> CreateAccount(Account userAccount);
        Task<string> GetAdminAccount(string email, string password);
    }
}
