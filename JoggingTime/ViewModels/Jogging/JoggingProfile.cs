using AutoMapper;

namespace JoggingTime.ViewModels.Jogging
{
    public class JoggingProfile: Profile
    {
        public JoggingProfile()
        {
            CreateMap<Models.UserJoggingTime, JoggingViewModel>()
                .ForMember(destination => destination.UserName,
                   opts => opts.MapFrom(source => source.User.Name)); ;
            CreateMap<Models.UserJoggingTime, JoggingCreateViewModel>().ReverseMap();
            CreateMap<Models.UserJoggingTime, JoggingUpdateViewModel>().ReverseMap();

        }
    }
}
