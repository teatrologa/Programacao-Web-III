using Microsoft.AspNetCore.Mvc.Filters;

namespace Aula_03.Filters
{
    public class LogResourceFilter : IResourceFilter
    {
        //usado DEPOIS de executar
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource/Recurso LogResourceFilter (DEPOIS) OnResourceExecuted");
        }

        //usado ANTES de executar
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Filtro de Resource/Recurso LogResourceFilter (ANTES) OnResourceExecuting");

            if (!context.HttpContext.Request.Headers.Keys.Contains("Code"))
            {
                context.HttpContext.Request.Headers.Add("Code", Guid.NewGuid().ToString());
            }
        }
    }
}
