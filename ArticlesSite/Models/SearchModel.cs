using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Collections.Generic;


namespace ArticlesSite.Models
{
    public class SearchModel : SearchResultItem
    {
        [IndexField("_name")]
        public virtual string ItemName { get; set; }

        [IndexField("articlesmallimage_t_en")]
        public virtual string smallimage { get; set; }

        [IndexField("shortdescription_t")]
        public virtual string shortDescription { get; set; } // Custom field on my template

        [IndexField("title_t")]
        public virtual string Title { get; set; } // Custom field on my template

        [IndexField("category_s")]
        public virtual string CategoryName { get; set; }
        
        [IndexField("posteddate_tdt")]
        public virtual string PostedDate { get; set; }

        [IndexField("id_t_en")]
        public virtual string Id { get; set; }
    }

    public class SearchResult
    {
        public string ItemName { get; set; }
        public string Title { get; set; }
        public string smallimage { get; set; }
        public string shortDescription { get; set; }
        public string CategoryName { get; set; }
        public string PostedDate { get; set; }

        public string id { get; set; }

    }
    /// <summary>
    /// Custom search result model for binding to front end
    /// </summary>
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; }
    }

}