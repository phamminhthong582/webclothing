using ModelLayer.DTO.Pagination;
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
        Task<List<AccountRespone>>GetAllAccountAsync();
        Task<AccountRespone>GetAccountById(int id);
        Task<AccountRespone> CreateAccount(CreateAccountRequest userAccount);
        Task<AccountRespone>UpdateAccount(UpdateAccountRequest request);
        Task DeleteAccountAsync(int id);
        Task<Account> GetAccountByEmail(string email);
        Task<string> GetAdminAccount(string email, string password);   
        Task <RepoRespone<string>> Login(string email, string password);
        //Task<Pagination<Account>> ToPagination(int pageindex = 0);
    }
}
