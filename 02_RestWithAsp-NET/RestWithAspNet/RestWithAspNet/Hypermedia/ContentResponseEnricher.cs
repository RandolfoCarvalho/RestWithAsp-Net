using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithAspNet.Hypermedia.Abstract;
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

        bool IResponseEnricher.canEnrich(ResultExecutingContext response)
        {
            if (response?.Result is OkObjectResult okObjectResult && okObjectResult?.Value != null)
            {
                var valueType = okObjectResult.Value.GetType();
                return CanEnrich(valueType);
            } else
            {
                throw new Exception("Erro ocorrido em enricher");
            }
            return false;
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
