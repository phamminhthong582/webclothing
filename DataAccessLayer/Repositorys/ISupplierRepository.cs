using ModelLayer.DTO.Request.Supplier;
using ModelLayer.DTO.Response.Supplier;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>>GetAllSupplierAsync();
        Task<SupplierRespone>GetSupplierById(int id);
        Task<SupplierRespone> CreateSupplier(CreateSupplierRequest supplierAccount);
        Task<SupplierRespone> UpdateSupplier(UpdateSupplierRequest request);
        Task DeleteSupplierAsync(int id);
        Task<List<Supplier>> SearchSuppliersByNameAsync(string supplierName);
        Task<List<Supplier>> SearchSuppliersByStatusAsync(bool? status);
        Task<List<Supplier>> SearchSuppliersByPhoneNumberAsync(string phoneNumber);
    }
}
