using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Application.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Tasks
{
    public class TaskRequestDto
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
    }

    public class UpdateTaskDtoValidator : AbstractValidator<TaskRequestDto>
    {
        public UpdateTaskDtoValidator()
        {
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
