using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Bussines.Accounts;

namespace WorkManager.Features.Authorization
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Nazwa użytkownika nie może być pusta")
                .MaximumLength(200).WithMessage("Nazwa użytkownika jest za długa");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Hasło nie może być puste");
        }
    }

    public class LoginRequestDtoMappingProfile : Profile
    {
        public LoginRequestDtoMappingProfile()
        {
            CreateMap<LoginRequestDto, LoginCommand>();
        }
    }
}
