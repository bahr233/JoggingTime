﻿using JoggingTime.ViewModels.User;

namespace JoggingTime.Services.User
{
    public interface IUserService
    {
        Models.User Create(UserCreateViewModel viewmodel);
        void Delete(int Id);
        List<UserViewModel> Get();
        UserViewModel GetById(int Id);
        void Update(UserUpdateViewModel viewmodel);
    }
}