
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
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

		public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
		{
            await bloggieDbContext.BlogPostsLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
		}

		public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
		{
			return await bloggieDbContext.BlogPostsLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostsLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
