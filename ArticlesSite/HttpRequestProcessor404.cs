using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;


namespace ArticlesSite
{
    public class HttpRequestProcessor404 : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Sitecore.Context.Item != null || Sitecore.Context.Site == null || Sitecore.Context.Database == null
                || Sitecore.Context.Database.Name == "core"
                || args.RequestUrl.AbsoluteUri.ToLower().Contains("/sitecore/api/layout/render/jss")
                || args.RequestUrl.AbsoluteUri.ToLower().Contains("/sitecore/api/jss/import")
                || args.RequestUrl.AbsoluteUri.ToLower().Contains("/api/sitecore/"))
            {
                return;
            }
            var pageNotFound = Sitecore.Context.Database.GetItem(@"{A1109D4E-309C-40C9-A842-6C4E11B5CF85}");
            if (pageNotFound == null)
                return;
            args.ProcessorItem = pageNotFound;
            Sitecore.Context.Item = pageNotFound;
            args.HttpContext.Response.StatusCode = 404;
        }


       
    }
}