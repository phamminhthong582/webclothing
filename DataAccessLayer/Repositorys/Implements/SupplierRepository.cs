using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Supplier;
using ModelLayer.DTO.Response.Supplier;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly WebCoustemClothingContext _context;
        public SupplierRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }
        public async Task<SupplierRespone> CreateSupplier(CreateSupplierRequest supplierAccount)
        {
            var sup = new Supplier
            {
                SupplierName = supplierAccount.SupplierName,
                Email = supplierAccount.Email,
                PhoneNumber = supplierAccount.PhoneNumber,
                Address = supplierAccount.Address,
                CreateDay = supplierAccount.CreateDay,
            };
            await _context.Suppliers.AddAsync(sup);
            await _context.SaveChangesAsync();
            var respone = new SupplierRespone
            {
                SupplierId = sup.SupplierId,
                SupplierName = sup.SupplierName,
                Email = sup.Email,
                Address = sup.Address,
                PhoneNumber = sup.PhoneNumber,
                CreateDay = DateTime.Now,
                Bank = supplierAccount.Bank,
                BankNumber = supplierAccount.BankNumber,
            };
            return respone;
        }
        

        public async Task DeleteSupplierAsync(int id)
        {
           var sup = await _context.Suppliers.FindAsync(id);
            if (sup != null)
            {
                _context.Suppliers.Remove(sup);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Supplier>> GetAllSupplierAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<SupplierRespone> GetSupplierById(int id)
        {
            var sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);
            if (sup != null)
            {
                return new SupplierRespone
                {
                    SupplierId = sup.SupplierId,
                    SupplierName = sup.SupplierName,
                    Address = sup.Address,
                    PhoneNumber = sup.PhoneNumber,
                    Email = sup.Email,
                    Bank = sup.Bank,
                    BankNumber = sup.BankNumber,
                    CreateDay = sup.CreateDay,
                };
            }
            return null;
        }

        public async Task<SupplierRespone> UpdateSupplier(UpdateSupplierRequest request)
        {
            var sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == request.SupplierId);
            if (sup != null)
            {
                sup.SupplierName = request.SupplierName;
                sup.Address = request.Address;
                sup.PhoneNumber = request.PhoneNumber;
            }
            await _context.SaveChangesAsync();
            var respone = new SupplierRespone
            {
                SupplierId = sup.SupplierId,
                SupplierName = sup.SupplierName,
                Address = sup.Address,
                PhoneNumber = sup.PhoneNumber,
                Email = sup.Email,
                Bank = sup.Bank,
                BankNumber = sup.BankNumber,
                CreateDay = DateTime.Now,
            };
            return respone;
        }
    }
}
