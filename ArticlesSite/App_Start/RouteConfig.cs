using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ArticlesSite.App_Start
{
    public class RouteConfig
    {

            public static void RegisterRoutes(RouteCollection routes)
            {
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                      name: "SearchForm",
                      url: "ket-qua/",
                      defaults: new { controller = "ArticleBody", action = "DoSearch" , id=UrlParameter.Optional}



                     );
            } 
    }
}