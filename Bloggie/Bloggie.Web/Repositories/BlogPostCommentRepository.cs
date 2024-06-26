using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
	public class BlogPostCommentRepository : IBlogPostCommentRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
			this.bloggieDbContext = bloggieDbContext;
		}
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
		{
			await bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
			await bloggieDbContext.SaveChangesAsync();
			return blogPostComment;
		}
	}
}
