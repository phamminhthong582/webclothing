using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO.Pagination;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Enum;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WebCoustemClothingContext _context;
        private readonly IConfiguration _configuration;
        protected readonly DbSet<Account> _dbSet;
        public AccountRepository(WebCoustemClothingContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _dbSet = _context.Set<Account>();
        }
        public async Task<List<AccountRespone>> GetAllAccountAsync()
        {
            return await _context.Accounts.Select(c => new AccountRespone
            {
                AccountId = c.AccountId,
                Address = c.Address,
                CreateDay = c.CreateDay,
                Email = c.Email,
                Password = c.Password,
                PhoneNumber = c.PhoneNumber,
                UserName = c.UserName
            }).AsNoTracking().ToListAsync();
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
        public async Task<string> GetAdminAccount(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            // Check if the configuration key exists
            if (config.GetSection("AdminAccount").Exists())
            {
                string emailJson = config["AdminAccount:adminemail"];
                string passwordJson = config["AdminAccount:adminpassword"];

                // Check if both email and password match
                if (emailJson == email && passwordJson == password)
                {
                    return emailJson;
                }
            }

            return null;
        }

        public async Task<RepoRespone<string>> Login(string email, string password)
        {
           var respone = new RepoRespone<string>();
           var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
           var admin = await GetAdminAccount(email , password);   
            if (user == null && admin == null)
            {
                respone.Success = false;
                respone.Message = "User not found!";
            }
            else if (admin != null)
            {
                respone.Success = true;
                respone.Message = "Admin login Successfully";
                respone.Data = CreateTokenForAdmin(admin);
            }
            else if (user.Password != password)
            {
                respone.Success = false;
                respone.Message = "Wrong password!";
            }           
            else
            {
                respone.Success = true;
                respone.Message = "Login Successfully";
                respone.Data = CreateToken(user);
            }

            return respone;
        }
        public string CreateToken(Account ua)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", ua.AccountId.ToString()),
                new Claim("Username", ua.UserName.ToString()),
                new Claim("Role", "User")

            };          
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:SerectKey"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string CreateTokenForAdmin(string email)
    {
        var claims = new List<Claim>()
        {
            new Claim("Email", email, SecurityAlgorithms.HmacSha256),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:SerectKey"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
            _configuration["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

        public async Task<Pagination<Account>> GetAllAccountPagination(int pageindex = 0)
        {
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet
                .Skip(pageindex * 5)
                .Include(c => c.AccountId).Include(c => c.Address)
                .Include(c => c.Email)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();
            var result = new Pagination<Account>()
            {
                PageIndex = pageindex,
                PageSize = 5,
                TotalItemCount = itemCount,
                Items = items,
            };

            return result;
        }
    }
}
