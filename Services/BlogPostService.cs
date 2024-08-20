using BlogManagementApi.Data;
using BlogManagementApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagementApi.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;

        public BlogPostService(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while retrieving all blog posts.", ex);
            }
        }

        public async Task<BlogPost> GetAsync(int id)
        {
            try
            {
                var post = await _repository.GetAsync(id);
                if (post == null)
                {
                    throw new KeyNotFoundException("Blog post not found.");
                }
                return post;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException($"An error occurred while retrieving the blog post with ID {id}.", ex);
            }
        }

        public async Task AddAsync(BlogPost post)
        {
            try
            {
                await _repository.AddAsync(post);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while adding the blog post.", ex);
            }
        }

        public async Task UpdateAsync(BlogPost post)
        {
            try
            {
                await _repository.UpdateAsync(post);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while updating the blog post.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while deleting the blog post.", ex);
            }
        }
    }
}