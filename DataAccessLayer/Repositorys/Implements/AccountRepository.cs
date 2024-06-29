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
        public AccountRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }
        public async Task<List<Account>> GetAllAccountAsync()
        {
            return await _context.Accounts.ToListAsync();
        }
        public async Task<AccountRespone> GetAccountById(int id)
        {
            var acc =  await _context.Accounts.FirstOrDefaultAsync(a =>a.AccountId == id);
            if(acc != null)
            {
                return new AccountRespone
                {
                    AccountId = acc.AccountId,
                    Email = acc.Email,
                    Password = acc.Password,
                    UserName = acc.UserName,
                    PhoneNumber = acc.PhoneNumber,
                    Address = acc.Address,
                    CreateDay = acc.CreateDay,
                };   
            }
            return null;
        }
        public async Task<AccountRespone> CreateAccount(CreateAccountRequest userAccount)
        {
            var acc = new Account
            {
                Email = userAccount.Email,
                Password = userAccount.Password,
                UserName = userAccount.UserName,
                PhoneNumber = userAccount.PhoneNumber,
                Address = userAccount.Address,
                CreateDay = DateTime.Now,
            };
            await _context.Accounts.AddAsync(acc);
            await _context.SaveChangesAsync();
            var respone = new AccountRespone
            {
                AccountId = acc.AccountId,
                Email = acc.Email,
                Password = acc.Password,
                UserName = acc.UserName,
                PhoneNumber = acc.PhoneNumber,
                Address = acc.Address,
                CreateDay = DateTime.Now,

            };  
            return respone;
        }

        public async Task<AccountRespone> UpdateAccount(UpdateAccountRequest request)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == request.AccountId);
            if (account != null)
            {
                account.Address = request.Address;
                account.UserName = request.UserName;
                account.PhoneNumber = request.PhoneNumber;
            } 
                await _context.SaveChangesAsync();
                var respone = new AccountRespone
                {
                    AccountId = account.AccountId,
                    Email = account.Email,
                    Password = account.Password,
                    UserName = account.UserName,
                    PhoneNumber = account.PhoneNumber,
                    Address = account.Address,
                    CreateDay = DateTime.Now,

                };
                return respone;
        }
        public async Task DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }
        public Task<string> GetAdminAccount(string email, string password)
        {
            throw new NotImplementedException();
        }  
    }
}
