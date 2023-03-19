using JoggingTime.ViewModels;
using JoggingTime.ViewModels.User;

namespace JoggingTime.Mediator.User
{
    public interface IUserMediator
    {
        int Create(UserCreateViewModel viewmodel);
        bool Delete(int id);
        List<UserViewModel> Get();
        UserViewModel GetById(int id);
        ResponseViewModel<string> Login(LoginViewModel viewmodel);
        bool LogOut(string token);
        bool Update(UserUpdateViewModel viewmodel);
    }
}
