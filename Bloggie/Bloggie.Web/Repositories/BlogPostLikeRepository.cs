
using Bloggie.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostsLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
