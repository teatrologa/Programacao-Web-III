using Microsoft.AspNetCore.Mvc.Filters;

namespace Aula_03.Filters
{
    public class LogResultFilter : ResultFilterAttribute
        //Aqui, deixei de implementar a interface do filtro (IResultFilter) para implementar
        // uma espécie de classe dele, onde posso sobrescrever seus métodos e usar apenas aqueles
        // que me convieram, ou só de antes ou só de depois etc. Nesse caso, existia um ANTES e um DEPOIS
    {
        public override void OnResultExecuted(ResultExecutedContext context)
            //É necessário colocar o everride nesses casos.
        {
            Console.WriteLine("Filtro de Result LogResultFilter (DEPOIS) OnResultExecuted");
        }

    }
}
