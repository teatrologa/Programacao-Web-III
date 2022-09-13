using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aula_03.Filters
{
    public class LogAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("Filtro de autorização logAuthorizationFilter OnAuthorization");
            
            context.HttpContext.Request.Headers.TryGetValue("User", out var usuario);
            if (String.IsNullOrEmpty(usuario))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            
        }
    }
}
