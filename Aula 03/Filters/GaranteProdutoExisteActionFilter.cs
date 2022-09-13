using Aula_03_Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Aula_03.Filters
{
    public class GaranteProdutoExisteActionFilter : ActionFilterAttribute
    //ESSE FILTRO PASSOU A SER UM SERVIÇO, logo precisa ser colocado como injeção de dep
    // la na program (igual formato).
    {
        public IProdutoService _produtoService;
        public GaranteProdutoExisteActionFilter(IProdutoService produtoService)
        {
            _produtoService = produtoService;
            //sempre que usamos injeção, precisamos registrar na program para que ela instancie
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idProduto = (long)context.ActionArguments["id"];

            if (_produtoService.ConsultarIdProduto(idProduto) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
