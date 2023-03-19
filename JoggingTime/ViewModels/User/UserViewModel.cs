using JoggingTime.Enums;

namespace JoggingTime.ViewModels.User
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
}
