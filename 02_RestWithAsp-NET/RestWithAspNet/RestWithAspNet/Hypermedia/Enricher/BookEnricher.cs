﻿using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Hypermedia.Constants;
using System.Text;

namespace RestWithAspNet.Hypermedia.Enricher
{
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/books/v1";
            string link = getLink(content.Id, urlHelper, path);
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return null;
        }

        private string getLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { Controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            };
        }
    }
}
