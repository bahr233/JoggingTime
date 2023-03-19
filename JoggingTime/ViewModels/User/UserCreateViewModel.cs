using JoggingTime.Enums;

namespace JoggingTime.ViewModels.User
{
    public class UserCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
}
