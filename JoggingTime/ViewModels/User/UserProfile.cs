using AutoMapper;

namespace JoggingTime.ViewModels.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.User, UserViewModel>().ReverseMap();
            CreateMap<Models.User, UserCreateViewModel>().ReverseMap();
            CreateMap<Models.User, UserUpdateViewModel>().ReverseMap();

        }
    }
}
