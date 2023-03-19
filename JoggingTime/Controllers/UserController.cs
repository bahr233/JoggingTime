using JoggingTime.Mediator.User;
using JoggingTime.ViewModels;
using JoggingTime.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace JoggingTime.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserMediator _userMediator;
        public UserController(IUserMediator userMediator)
        {
            _userMediator = userMediator;
        }

        [HttpGet]
        public ResponseViewModel<List<UserViewModel>> Get()
        {
            return new ResponseViewModel<List<UserViewModel>>(_userMediator.Get());
        }
        [HttpGet]
        public ResponseViewModel<UserViewModel> GetById(int id)
        {
            return new ResponseViewModel<UserViewModel>(_userMediator.GetById(id));
        }
        [HttpPost]
        public ResponseViewModel<int> Create(UserCreateViewModel viewmodel)
        {
            return new ResponseViewModel<int>(_userMediator.Create(viewmodel), "User Added Successfuly");
        }
        [HttpPost]
        public ResponseViewModel<string> LogIn(LoginViewModel viewmodel)
        {
            return _userMediator.Login(viewmodel);
        }
        [HttpPost]
        public ResponseViewModel<bool> LogOut(string token)
        {
            return new ResponseViewModel<bool>(_userMediator.LogOut( token));
        }
        [HttpPut]
        public ResponseViewModel<bool> Update(UserUpdateViewModel viewmodel)
        {
            return new ResponseViewModel<bool>(_userMediator.Update(viewmodel), "User Updated Successfuly");
        }
        [HttpDelete]
        public ResponseViewModel<bool> Delete(int id)
        { 
            return new ResponseViewModel<bool>(_userMediator.Delete(id), "User Deleted Successfuly");
        }
    }
}
