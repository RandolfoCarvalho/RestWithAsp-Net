using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithAspNet.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool canEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
