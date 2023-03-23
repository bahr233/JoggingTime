using JoggingTime.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JoggingTime.Filters
{
    public class AuthenticateFilter : ActionFilterAttribute
    {
        private const string tokenHeaderName = "token";
        private object[] _roles;

        public AuthenticateFilter(params object[] roles)
        {
            _roles = roles;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IsAuthenticated();
        }
        public bool IsAuthenticated()
        {
            var authenticated = HttpRequestHelper.IsHeaderContainsKey(tokenHeaderName);
            if (!authenticated)
            {
                throw new Exception("من فضلك قم بتسجيل الدخول.");
            }
            return authenticated;
        }
    }
}
