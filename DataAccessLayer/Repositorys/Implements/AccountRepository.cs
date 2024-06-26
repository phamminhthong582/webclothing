using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Enum;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WebCoustemClothingContext _context;
        protected readonly DbSet<Account> _accounts;
        
        public async Task<Account> CreateAccount(Account userAccount)
        {   
            await _context.Accounts.AddAsync(userAccount);
            _context.SaveChanges();
            return userAccount;
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            account.Status = AccountStatus.InActive.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Set<Account>().FirstOrDefaultAsync(c => c.Email.ToLower().Equals(email.ToLower()));
        }

        public async Task<AccountRespone> GetAccountById(int id)
        {
            var acc = await _accounts.Select(c => new AccountRespone
            {
                AccountId = c.AccountId,
                Email = c.Email,
                Address = c.Address,
            }).FirstOrDefaultAsync();
            return acc;
        }

        public Task<string> GetAdminAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Account>> GetAllAccountAsync()
        {
            return await _context.Accounts.Include(c => c.AccountId).ToListAsync();
        }

        public Task<Account> UpdateAccount(UpdateAccountRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
