using JoggingTime.Enums;
using JoggingTime.Helpers;
using JoggingTime.Services.Token;
using JoggingTime.Services.User;
using JoggingTime.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Filters;
namespace JoggingTime.Filters
{
    public class AuthorizeRoleFilter :ActionFilterAttribute
    {
        private const string tokenHeaderName = "token";
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserRole _Role;


        public AuthorizeRoleFilter(
            UserRole Role,
            IUserService userService,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
             _Role= Role;
            _userService = userService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = HttpRequestHelper.GetHeaderValue(tokenHeaderName);
         
            try
            {
                //if (SecurityHelper.IsTokenExpired(token))
                //    throw new AuthenticatedException("غير مسموح بالدخول");

                int userID = SecurityHelper.GetUserIDFromToken(token);
                if (userID <= 0)
                    throw new Exception("غير مسموح بالدخول");

                IsAuthenticatedfn();
                if (!IsValidTokenfn(token))
                {
                    throw new Exception("غير مسموح بالدخول");
                }
                IsAuthorizedfn(userID);
            }
            catch (Exception )
            {
                throw new Exception("حدث خطا ما");
            }
            base.OnActionExecuting(context);
        }
        public bool IsValidTokenfn(string token)
        {
            return _tokenService.IsValidToken(token);
        }
        public bool IsAuthenticatedfn()
        {
            var authenticated = HttpRequestHelper.IsHeaderContainsKey(tokenHeaderName);
            if (!authenticated)
            {
                throw new Exception("من فضلك قم بتسجيل الدخول.");
            }
            return authenticated;
        }

        public bool IsAuthorizedfn(int userID)
        {
            if (_userService.HasAccess(userID, _Role))
                throw new Exception("not authorized action");
            return true;
        }
      
    }
}
