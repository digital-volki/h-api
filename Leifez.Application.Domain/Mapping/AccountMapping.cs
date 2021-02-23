using AutoMapper;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Leifez.Application.Domain.Mapping
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<DbIdentityUser, Account>()
                .ForMember(a => a.AccountId, opt => opt.MapFrom(b => b.Id))
                .ForMember(a => a.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(a => a.UserName, opt => opt.MapFrom(b => b.UserName));

            CreateMap<Account, DbIdentityUser>()
                .ForMember(a => a.Id, opt => opt.MapFrom(b => b.AccountId))
                .ForMember(a => a.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(a => a.UserName, opt => opt.MapFrom(b => b.UserName));
        }
    }
}
