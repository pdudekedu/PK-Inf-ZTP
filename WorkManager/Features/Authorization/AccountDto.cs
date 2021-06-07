using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Data.Entities;

namespace WorkManager.Features.Authorization
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
    }

    public class AccountDtoMappingProfile : Profile
    {
        public AccountDtoMappingProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
