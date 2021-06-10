using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Application.Users;

namespace WorkManager.Presentation.Users
{
    public class UpdatePasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }

    public class UpdatePasswordRequestDtoValidator : AbstractValidator<UpdatePasswordRequestDto>
    {
        public UpdatePasswordRequestDtoValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().MaximumLength(200).WithMessage("Aktualne hasło nie może być puste");
            RuleFor(x => x.Password).NotEmpty().MaximumLength(200).WithMessage("Hasło nie może być puste");
        }
    }

    public class UpdatePasswordRequestDtoMappingProfile : Profile
    {
        public UpdatePasswordRequestDtoMappingProfile()
        {
            CreateMap<UpdatePasswordRequestDto, UpdateCurrentUserPasswordCommand>();
        }
    }
}
