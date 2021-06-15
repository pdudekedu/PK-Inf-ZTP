using AutoMapper;
using FluentValidation;
using WorkManager.Application.Resources;

namespace WorkManager.Presentation.Resources
{
    public class ResourceRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateResourceDtoValidator : AbstractValidator<ResourceRequestDto>
    {
        public UpdateResourceDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa nie może być pusta")
                .MaximumLength(200).WithMessage("Nazwa nie może być dłuższa niż 200 znaków");
        }
    }

    public class UpdateResourceDtoMappingProfile : Profile
    {
        public UpdateResourceDtoMappingProfile()
        {
            CreateMap<ResourceRequestDto, CreateResourceCommand>();
            CreateMap<ResourceRequestDto, UpdateResourceCommand>();
        }
    }
}
