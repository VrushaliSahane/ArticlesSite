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
    public class SearchController : Controller
    {
        string query = string.Empty;
        // GET: Search
   

        [HttpPost]
        public ActionResult searchPredicate(FormCollection form)
        {
            query = form["searchInput"];
            List<Sitecore.Data.Items.Item> blogItems = new List<Sitecore.Data.Items.Item>();
            List<BlogCard> BlogCardsCollection = new List<BlogCard>();
            Item home = Sitecore.Context.Database.GetItem("{ED70E021-A80C-472E-B0E2-9513C4931979}");

            var Websitesettings = Sitecore.Context.Database.GetItem("{319231EA-9BD8-4EE8-A456-2D99D661B554}");

            blogItems = home.Axes.GetDescendants().ToList();

            for (int i = 0; i < blogItems.Count; i++)
            {
                BlogCard BlogModel = new BlogCard();
                var imageUrl = string.Empty;
                if (!(blogItems[i].HasChildren) && blogItems[i].TemplateName.Contains("Article"))
                {
                    Sitecore.Data.Fields.ImageField imageField = blogItems[i].Fields["ArticleSmallImage"];
                    if (imageField?.MediaItem != null)
                    {
                        var image = new MediaItem(imageField.MediaItem);
                        imageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
                        BlogModel.BlogSImage = imageUrl;
                    }
                    BlogModel.Category = blogItems[i].Fields["Category"].Value;
                    BlogModel.BlogTitle = blogItems[i].Fields["Title"].Value;

                    Sitecore.Data.Fields.DateField dateTimeField = blogItems[i].Fields["PostedDate"];

                    string dateTimeString = dateTimeField.Value;

                    DateTime dateTimeStruct = Sitecore.DateUtil.IsoDateToDateTime(dateTimeString);
                    BlogModel.date = dateTimeStruct.ToShortDateString();

                    BlogModel.ID = blogItems[i].Fields["Id"].Value;
                    BlogModel.ShortDesc = blogItems[i].Fields["ShortDescription"].Value;
                    BlogModel.LongtDesc = blogItems[i].Fields["LongDescription"].Value;
                    BlogModel.Readonbtn = Websitesettings.Fields["PostCardButtonText"].Value;
                    BlogModel.sitecoreItem = blogItems[i];
                    BlogModel.BlogURL = Sitecore.Links.LinkManager.GetItemUrl(blogItems[i]);



                    BlogCardsCollection.Add(BlogModel);
                }
            }
                if (query is null)
                    query = "hi";

                List<BlogCard> results = BlogCardsCollection.FindAll(Findtitle);



                bool Findtitle(BlogCard bk)
                {

                    if (bk.BlogTitle.Contains(query) || bk.Category.Contains(query) || bk.ShortDesc.Contains(query) || bk.LongtDesc.Contains(query))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                ViewBag.searchCards = results;
                return View("/Views/Search/SearchCards.cshtml", ViewBag.searchCards);
            }
        }
}