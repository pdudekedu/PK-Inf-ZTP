using AutoMapper;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using WorkManager.Application.Projects;

namespace WorkManager.Presentation.Projects
{
    public class ProjectRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<NamesDto> Resources { get; set; }
        public NamesDto Team { get; set; }
    }

    public class UpdateProjectDtoValidator : AbstractValidator<ProjectRequestDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa nie może być pusta")
                .MaximumLength(200).WithMessage("Nazwa nie może być dłuższa niż 200 znaków");

            RuleFor(x => x.Team)
                .Must(x => x != null)
                .WithMessage("Projekt musi mieć przypisany zespół");
        }
    }

    public class UpdateProjectDtoMappingProfile : Profile
    {
        public UpdateProjectDtoMappingProfile()
        {
            CreateMap<ProjectRequestDto, CreateProjectCommand>()
                .ForMember(dest => dest.Resources, opt =>
                {
                    opt.Condition(x => x.Resources != null);
                    opt.MapFrom(x => x.Resources.Select(x => x.Id));
                })
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(x => x.Team.Id));

            CreateMap<ProjectRequestDto, UpdateProjectCommand>()
                .ForMember(dest => dest.Resources, opt =>
                {
                    opt.Condition(x => x.Resources != null);
                    opt.MapFrom(x => x.Resources.Select(x => x.Id));
                })
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(x => x.Team.Id));
        }
    }
}
