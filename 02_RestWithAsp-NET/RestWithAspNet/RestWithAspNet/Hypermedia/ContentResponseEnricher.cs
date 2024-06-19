using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithAspNet.Hypermedia.Abstract;
using Serilog;
using System.Collections.Concurrent;

namespace RestWithAspNet.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportHypermedia
    {
        public ContentResponseEnricher()
        {

        }
        public virtual bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        bool IResponseEnricher.canEnrich(ResultExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "O contexto da resposta é nulo");
            }

            if (context.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value == null)
                {
                    // Valor do OkObjectResult é nulo
                    Log.Warning("O valor do OkObjectResult é nulo");
                    return false; // Ou lance uma exceção dependendo da necessidade
                }

                var valueType = okObjectResult.Value.GetType();
                return CanEnrich(valueType);
            }
            else if (context.Result is NoContentResult)
            {
                // NoContentResult significa que não há conteúdo para ser retornado
                Log.Information("NoContentResult: Não há conteúdo para enriquecer");
                return false;
            }
            else
            {
                // Resultado inesperado
                throw new InvalidOperationException("O resultado não é do tipo OkObjectResult ou NoContentResult");
            }
        }
        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }
            await Task.FromResult<object>(null);
        }
    }
}
