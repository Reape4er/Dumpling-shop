using Microsoft.Identity.Client;
using Users.API.Services;

namespace Users.API.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        public JWTMiddleware (RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext, IUserService userService, IJWTUtils JWTutils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userid = JWTutils.ValidateJWTToken(token);

            if (userid != null)
            {
                httpContext.Items["user"] = await userService.GetUserById(userid.Value);
            }
            await _next(httpContext);   
        }
    }
}
