using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticlesSite.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization.ObjectModel;

namespace ArticlesSite.Models
{
    public class BlogCard
    {
        public string BlogSImage { get; set; }
        public string ID { get; set; }
        public string BlogTitle { get; set; }
        public string Category { get; set; }
        public string date { get; set; }

        public string LongtDesc { get; set; }
        public string BlogLImage { get; set; }
        public string ShortDesc { get; set; }

        public string BlogURL {get;set;}
        public Item sitecoreItem { get; set; }
        public string Readonbtn { get; set; }
    }
}