using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImgUrl { get; set; }
        public string UrlHandler { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
    }
}
