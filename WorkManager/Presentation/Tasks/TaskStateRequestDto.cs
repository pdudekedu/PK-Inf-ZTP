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
    public class TaskStateRequestDto
    {
        public TaskState State { get; set; }
    }


    public class TaskStateRequestDtoMappingProfile : Profile
    {
        public TaskStateRequestDtoMappingProfile()
        {
            CreateMap<TaskStateRequestDto, UpdateTaskStateCommand>();
        }
    }
}
