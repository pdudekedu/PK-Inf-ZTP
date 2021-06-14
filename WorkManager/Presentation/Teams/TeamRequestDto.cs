using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Application.Teams;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Teams
{
    public class TeamRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserDto> Users { get; set; }
    }

    public class UpdateTeamDtoValidator : AbstractValidator<TeamRequestDto>
    {
        public UpdateTeamDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa nie może być pusta")
                .MaximumLength(200).WithMessage("Nazwa nie może być dłuższa niż 200 znaków");

            RuleFor(x => x.Users)
                .Must(x => x?.Any() == true)
                .WithMessage("Zespół musi mieć przynajmniej jednego użytkownika");
        }
    }

    public class UpdateTeamDtoMappingProfile : Profile
    {
        public UpdateTeamDtoMappingProfile()
        {
            CreateMap<TeamRequestDto, CreateTeamCommand>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(x => x.Users.Select(x => x.Id)));
            CreateMap<TeamRequestDto, UpdateTeamCommand>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(x => x.Users.Select(x => x.Id)));
        }
    }
}
