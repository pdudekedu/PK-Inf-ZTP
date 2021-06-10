using AutoMapper;
using FluentValidation;
using WorkManager.Application.Users;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Users
{
    public class UpdateUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserRequestDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).WithMessage("Imię nie może być puste");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).WithMessage("Nazisko nie może być puste");
            RuleFor(x => x.Role).IsInEnum().WithMessage("Rola nie jest poprawną wartością");
        }
    }

    public class UpdateUserRequestDtoMappingProfile : Profile
    {
        public UpdateUserRequestDtoMappingProfile()
        {
            CreateMap<UpdateUserRequestDto, UpdateUserCommand>();
        }
    }
}
