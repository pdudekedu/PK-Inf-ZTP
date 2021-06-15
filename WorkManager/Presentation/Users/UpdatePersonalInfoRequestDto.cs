using AutoMapper;
using FluentValidation;
using WorkManager.Application.Users;

namespace WorkManager.Presentation.Users
{
    public class UpdatePersonalInfoRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdatePersonalInfoRequestDtoValidator : AbstractValidator<UpdatePersonalInfoRequestDto>
    {
        public UpdatePersonalInfoRequestDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).WithMessage("Imię nie może być puste");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).WithMessage("Nazisko nie może być puste");
        }
    }

    public class UpdatePersonalInfoRequestDtoMappingProfile : Profile
    {
        public UpdatePersonalInfoRequestDtoMappingProfile()
        {
            CreateMap<UpdatePersonalInfoRequestDto, UpdateCurrentUserPersonalInfoCommand>();
        }
    }
}
