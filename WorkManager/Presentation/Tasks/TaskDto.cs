using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Tasks
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public TaskState State { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
    }

    public class TaskDtoMappingProfile : Profile
    {
        public TaskDtoMappingProfile()
        {
            CreateMap<Persistence.Entities.Task, TaskDto>();
        }
    }
  
}
