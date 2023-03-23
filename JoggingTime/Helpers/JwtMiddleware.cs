using JoggingTime.Enums;
using JoggingTime.Services.Token;
using JoggingTime.Services.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JoggingTime.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, ITokenService tokenService)
        {
            var token = context.Request.Headers["Token"].FirstOrDefault()?.Split("").Last();

            if (token != null && tokenService.IsValidToken(token))
            {
                //token = SecurityHelper.Decrypt(token);
                attachUserToContext(context, userService, token);

            }

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken); ; ;

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                UserRole roleID = (UserRole)int.Parse(jwtToken.Claims.First(x => x.Type == "RoleID").Value);

                context.Items["UserID"] = userId; 
                context.Items["RoleID"] = roleID;
           
            }
            catch (Exception)
            {
               
            }
        }

    }
    public class JWTSettings
    {
        public string Secret { get; set; }
    }
}
