using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using WorkManager.Application.Tasks;

namespace WorkManager.Presentation.Tasks
{
    public class TaskRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
        public IEnumerable<ResourceDto> Resources { get; set; }
        public UserDto User { get; set; }
    }

    public class UpdateTaskDtoValidator : AbstractValidator<TaskRequestDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x)
                .Must(x => !x.EstimateStart.HasValue || !x.EstimateEnd.HasValue || x.EstimateEnd >= x.EstimateStart)
                .WithMessage("Data rozpoczęcia nie może być mniejsza od daty końca.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa nie może być pusta")
                .MaximumLength(200).WithMessage("Nazwa nie może być dłuższa niż 200 znaków");
        }
    }

    public class UpdateTaskDtoMappingProfile : Profile
    {
        public UpdateTaskDtoMappingProfile()
        {
            CreateMap<TaskRequestDto, CreateTaskCommand>();
            CreateMap<TaskRequestDto, UpdateTaskCommand>();
        }
    }
}
