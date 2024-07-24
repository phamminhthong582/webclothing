using AutoMapper;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO.Response.Products;

namespace DataAccessLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountRespone>()
            .ForMember(c => c.AccountId, opt => opt.MapFrom(a => a.AccountId))
            .ForMember(c => c.Email, opt => opt.MapFrom(a => a.Email))
            .ForMember(c => c.UserName, opt => opt.MapFrom(a => a.UserName))
            .ForMember(c => c.Password, opt => opt.MapFrom(a => a.Password))
            .ForMember(c => c.Address, opt => opt.MapFrom(a => a.Address))
            .ForMember(c => c.PhoneNumber, opt => opt.MapFrom(a => a.PhoneNumber))
            .ForMember(c => c.CreateDay, opt => opt.MapFrom(a => a.CreateDay));
            CreateMap<UpdateAccountRequest, Account>()
            .ForMember(c => c.UserName, opt => opt.MapFrom(a => a.UserName))
            .ForMember(c => c.Address, opt => opt.MapFrom(a => a.Address));
            CreateMap<CreateAccountRequest, Account>()           
            .ForMember(c => c.UserName, opt => opt.MapFrom(a => a.UserName))
            .ForMember(c => c.Address, opt => opt.MapFrom(a => a.Address))
            .ForMember(c => c.UserName, opt => opt.MapFrom(a => a.UserName))        
            .ForMember(c => c.Email, opt => opt.MapFrom(a => a.Email))
            .ForMember(c => c.Password, opt => opt.MapFrom(a => a.Password));
            
            CreateMap<Product, ProductRespone>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty))
                .ReverseMap();
        }
    }
}
