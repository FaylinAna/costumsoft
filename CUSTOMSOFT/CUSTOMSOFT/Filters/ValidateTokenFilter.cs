using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CUSTOMSOFT.API.Filters
{
    public class ValidateTokenFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
            {
                var token = context.HttpContext.Request.Headers["Authorization"];

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadToken(token.ToString().Replace("Bearer ","").Trim()) as JwtSecurityToken;

                        if (jwtToken.ValidTo < DateTime.UtcNow)
                        { 
                            context.Result = new UnauthorizedResult();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
