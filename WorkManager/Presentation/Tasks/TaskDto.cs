using AutoMapper;
using System;
using System.Collections.Generic;
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
        public IEnumerable<ResourceDto> Resources { get; set; }
        public UserDto User { get; set; }
    }

    public class TaskDtoMappingProfile : Profile
    {
        public TaskDtoMappingProfile()
        {
            CreateMap<Persistence.Entities.Task, TaskDto>();

            CreateMap<User, UserDto>();
            CreateMap<Resource, ResourceDto>();
        }
    }
    public class ResourcesResolver : IValueResolver<Task, TaskDto, IEnumerable<ResourceDto>>
    {
        public IEnumerable<ResourceDto> Resolve(Task source, TaskDto destination, IEnumerable<ResourceDto> destMember, ResolutionContext context)
        {
            if (source.Resources != null)
                foreach (var item in source.Resources)
                {
                    yield return new()
                    {
                        Name = item.Name,
                        Id = item.Id
                    };
                }

        }
    }
}
