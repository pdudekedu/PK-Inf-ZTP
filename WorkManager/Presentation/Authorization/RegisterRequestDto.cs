using AutoMapper;
using FluentValidation;
using WorkManager.Application.Users;

namespace WorkManager.Presentation.Authorization
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(200).WithMessage("Nazwa użytkownika nie może być pusta");
            RuleFor(x => x.Password).NotEmpty().MaximumLength(200).WithMessage("Hasło nie może być puste");
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).WithMessage("Imię nie może być puste");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).WithMessage("Nazisko nie może być puste");
        }
    }

    public class RegisterRequestDtoMappingProfile : Profile
    {
        public RegisterRequestDtoMappingProfile()
        {
            CreateMap<RegisterRequestDto, RegisterCommand>();
        }
    }
}
