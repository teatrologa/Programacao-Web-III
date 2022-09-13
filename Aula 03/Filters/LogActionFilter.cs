using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aula_03.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        //MÉTODO original quando implementada a interface, sem o override.
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    Console.WriteLine("Filtro de ação LogActionFilter (DEPOIS) OnActionExecuted");
        //}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Filtro de ação LogActionFilter (ANTES) OnActionExecuting");

            if (!context.ModelState.IsValid)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);

            }
        } 
    }
}
