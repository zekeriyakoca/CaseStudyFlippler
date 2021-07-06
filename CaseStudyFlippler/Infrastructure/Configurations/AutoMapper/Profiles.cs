using AutoMapper;
using CaseStudyFlippler.Application.Dtos;
using CaseStudyFlippler.Application.Entities;

namespace CaseStudyFlippler.API.Infrastructure.Configurations.AutoMapper
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Reminder, ReminderDto>()
                .ReverseMap();
            CreateMap<Reminder, ReminderCreateDto>()
                .ReverseMap();

            CreateMap<Reminder, ReminderUpdateDto>()
                .ReverseMap();

        }
    }
}
