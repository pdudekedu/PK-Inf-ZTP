using AutoMapper;
using System;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Reports
{
    public class ProjectStatisticDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int New { get; set; }
        public int Active { get; set; }
        public int Suspend { get; set; }
        public int Complete { get; set; }
        public int State { get; set; }
        public string Team { get; set; }
        public TimeSpan WorkTime { get; set; }
        public TimeSpan EstimateWorkTime { get; set; }
        public double Punctuality { get; set; }
    }
    public class ProjectStatisticDtoMappingProfile : Profile
    {
        public ProjectStatisticDtoMappingProfile()
        {
            CreateMap<ProjectStatistic, ProjectStatisticDto>();
        }
    }
}
