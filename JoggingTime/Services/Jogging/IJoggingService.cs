﻿using JoggingTime.Models;
using JoggingTime.ViewModels.Jogging;

namespace JoggingTime.Services.Jogging
{
    public interface IJoggingService
    {
        UserJoggingTime Create(JoggingCreateViewModel viewmodel);
        void Delete(int Id);
        List<JoggingViewModel> Get();
        JoggingViewModel GetById(int Id);
        void Update(JoggingUpdateViewModel viewmodel);
    }
}
