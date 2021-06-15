using AutoMapper;
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
