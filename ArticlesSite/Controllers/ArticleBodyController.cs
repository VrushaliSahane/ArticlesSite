using ArticlesSite.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using Sitecore.Resources.Media;
using Sitecore;
using System.Web.UI.MobileControls;
using Sitecore.Data;

namespace ArticlesSite.Controllers
{
    public class ArticleBodyController : Controller
    {
        // GET: ArticleBody

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetArticleComponent()
        {
            BlogCard bg = new BlogCard();
            string CurrentURL = Request.Url.AbsoluteUri;

            string path = "/sitecore/content/BlogWeb/Home" + Request.Url.AbsolutePath;


            var item2 = Sitecore.Context.Database.GetItem(path);



            bg.BlogLImage = item2.Fields["ArticleLargeImage"].Value;
            bg.LongtDesc = item2.Fields["LongDescription"].Value;
            bg.BlogTitle = item2.Fields["Title"].Value;
            bg.date = item2.Fields["PostedDate"].Value;
            bg.Category = item2.Fields["Category"].Value;
            bg.BlogURL = path;


            return View("~/Views/Articles/ArticleBody.cshtml", bg);
        }



        public ActionResult GetPreferedTemplate()

        {
            Item home = Sitecore.Context.Database.GetItem("{ED70E021-A80C-472E-B0E2-9513C4931979}");

            var Websitesettings = Sitecore.Context.Database.GetItem("{319231EA-9BD8-4EE8-A456-2D99D661B554}"); // WebHome

            List<BlogCard> BlogCardsCollection = new List<BlogCard>();

            List<Sitecore.Data.Items.Item> blogItems = new List<Sitecore.Data.Items.Item>();
            blogItems = home.Axes.GetDescendants().ToList();
            List<Sitecore.Data.Items.Item> PassDatablogItems = new List<Sitecore.Data.Items.Item>();
            List<BlogCard> bc = new List<BlogCard>();
            for (int i = 0; i < blogItems.Count; i++)
            {
                if (blogItems[i].Fields["ShortDescription"] != null)
                {
                    BlogCard BlogModel = new BlogCard();
                    var imageUrl = string.Empty;
                    Sitecore.Data.Fields.ImageField imageField = blogItems[i].Fields["ArticleSmallImage"];
                    if (imageField?.MediaItem != null)
                    {
                        var image = new MediaItem(imageField.MediaItem);
                        imageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
                        BlogModel.BlogSImage = imageUrl;
                    }
                    BlogModel.Category = blogItems[i].Fields["Category"].Value;
                    BlogModel.BlogTitle = blogItems[i].Fields["Title"].Value;


                    BlogModel.ID = blogItems[i].Fields["Id"].Value;
                    Sitecore.Data.Fields.DateField dateTimeField = blogItems[i].Fields["PostedDate"];
                    string dateTimeString = dateTimeField.Value;
                    DateTime dateTimeStruct = Sitecore.DateUtil.IsoDateToDateTime(dateTimeString);
                    BlogModel.date = dateTimeStruct.ToShortDateString();
                    BlogModel.ShortDesc = blogItems[i].Fields["ShortDescription"].Value;
                    BlogModel.Readonbtn = Websitesettings.Fields["PostCardButtonText"].Value;
                    BlogModel.sitecoreItem = blogItems[i];
                    BlogModel.BlogURL = Sitecore.Links.LinkManager.GetItemUrl(blogItems[i]);

                    BlogCardsCollection.Add(BlogModel);
                    bc.Add(BlogModel);
                }
            }
            ViewBag.BlogCards = BlogCardsCollection;
            // return PartialView("~/Views/Blog_Harshit/DynamicCards.cshtml", ViewBag.BlogCards);

            return View("~/Views/Articles/HomesiteSettings.cshtml", bc);
        }
        public ActionResult HomesiteSettings(ID id)
        {
            var itemHome = Sitecore.Context.Database.GetItem(id);
            return View("~/Views/Articles/HomesiteSettings.cshtml", itemHome);
        }


       
      
    }
}

