using JoggingTime.Enums;
using JoggingTime.ViewModels.User;

namespace JoggingTime.Services.User
{
    public interface IUserService
    {
        Models.User Create(UserCreateViewModel viewmodel);
        void Delete(int Id);
        List<UserViewModel> Get();
        UserViewModel GetById(int Id);
        Models.User Login(LoginViewModel viewmodel);
        void Update(UserUpdateViewModel viewmodel);

        bool HasAccess(int userId, UserRole userRole);
    }
}
