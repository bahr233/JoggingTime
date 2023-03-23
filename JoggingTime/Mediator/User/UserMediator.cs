using JoggingTime.Services.User;
using JoggingTime.UnitOfWork;
using JoggingTime.ViewModels.User;
using JoggingTime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using JoggingTime.Services.Token;
using JoggingTime.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace JoggingTime.Mediator.User
{
    public class UserMediator: IUserMediator
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        public UserMediator(IUserService userService, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public List<UserViewModel> Get()
        {
            return _userService.Get();
        }
     
        public UserViewModel GetById(int id)
        {
            return _userService.GetById(id);
        }
       
        public int Create(UserCreateViewModel viewmodel)
        {
            viewmodel.Password = SecurityHelper.GetHashedPassword(viewmodel.Password);
            var model = _userService.Create(viewmodel);
            _unitOfWork.Save();
            return model.ID;
        }

      
        public bool Update(UserUpdateViewModel viewmodel)
        {
            viewmodel.Password = SecurityHelper.GetHashedPassword(viewmodel.Password);
            _userService.Update(viewmodel);
            _unitOfWork.Save();
            return true;
        }
     
        public bool Delete(int id)
        {
            _userService.Delete(id);
            _unitOfWork.Save();
            return true;
        }

     
        public ResponseViewModel<string> Login(LoginViewModel viewmodel)
        {
            viewmodel.Password = SecurityHelper.GetHashedPassword(viewmodel.Password);
            var user = _userService.Login(viewmodel);
            if (user == null)
            {
                return new ResponseViewModel<string>("","User name or password is incorrect",false,false);
            }
            else
            {
                var Usertoken =_tokenService.Get(user.ID);
                if (Usertoken == null)
                {
                    var model = new Models.Token()
                    {
                        UserID = user.ID,
                        IsDeleted = false,
                        ExpirationDate = DateTime.Now.AddDays(30),
                        Code = SecurityHelper.GenerateToken(user.ID)
                    };
                    var token = _tokenService.Add(model);
                    _unitOfWork.Save();
                    return new ResponseViewModel<string>(token);
                }

                return new ResponseViewModel<string>(Usertoken.Code);
            }
        }
        public bool LogOut(string token)
        {
            var userId =SecurityHelper.GetUserIDFromToken(token);
            _tokenService.Remove(userId);
            _unitOfWork.Save();
            return true;
        }


    }
}
