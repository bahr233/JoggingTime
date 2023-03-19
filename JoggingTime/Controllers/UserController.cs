using JoggingTime.Services.User;
using JoggingTime.UnitOfWork;
using JoggingTime.ViewModels;
using JoggingTime.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JoggingTime.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ResponseViewModel<List<UserViewModel>> Get()
        {
            return new ResponseViewModel<List<UserViewModel>>( _userService.Get());
        }
        [HttpGet]
        public ResponseViewModel<UserViewModel> GetById(int id)
        {
            return new ResponseViewModel<UserViewModel>(_userService.GetById(id));
        }
        [HttpPost]
        public ResponseViewModel<int> Create(UserCreateViewModel viewmodel)
        {
            var model = _userService.Create(viewmodel);
            _unitOfWork.Save();
            return new ResponseViewModel<int>(model.ID,"User Added Successfuly");
        } 

        [HttpPut]
        public ResponseViewModel<bool> Update(UserUpdateViewModel viewmodel)
        {
            _userService.Update(viewmodel);
            _unitOfWork.Save();
            return new ResponseViewModel<bool>(true, "User Updated Successfuly");
        }
        [HttpDelete]
        public ResponseViewModel<bool> Delete(int id)
        {
            _userService.Delete(id);
            _unitOfWork.Save();
            return new ResponseViewModel<bool>(true, "User Deleted Successfuly");
        }
    }
}
