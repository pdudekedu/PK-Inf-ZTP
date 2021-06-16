using AutoMapper;
using System;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Reports
{
    public class UserStatisticDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan WorkTime { get; set; }
        public TimeSpan EstimateWorkTime { get; set; }
        public double Punctuality { get; set; }
        public int TaskCount { get; set; }
        public int ProjectCount { get; set; }
    }

    public class UserStatisticDtoMappingProfile : Profile
    {
        public UserStatisticDtoMappingProfile()
        {
            CreateMap<UserStatistic, UserStatisticDto>();
        }
    }
}
