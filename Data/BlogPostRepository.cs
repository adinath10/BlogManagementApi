using BlogManagementApi.Models;
using System.Text.Json;

namespace BlogManagementApi.Data
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly string _filePath;

        public BlogPostRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<BlogPost>();
                }

                return JsonSerializer.Deserialize<List<BlogPost>>(await File.ReadAllTextAsync(_filePath)) ?? new List<BlogPost>();
            }
            catch (IOException ex)
            {
                // Log exception (consider using a logging framework)
                throw new ApplicationException("An error occurred while reading the blog posts.", ex);
            }
        }

        public async Task<BlogPost> GetAsync(int id)
        {
            try
            {
                var posts = await GetAllAsync();
                return posts.FirstOrDefault(post => post.Id == id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while retrieving the blog post.", ex);
            }
        }

        public async Task AddAsync(BlogPost post)
        {
            try
            {
                var posts = await GetAllAsync();
                post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
                posts.Add(post);
                await SaveAsync(posts);
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
                var posts = await GetAllAsync();
                var existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
                if (existingPost != null)
                {
                    posts.Remove(existingPost);
                    posts.Add(post);
                    await SaveAsync(posts);
                }
                else
                {
                    throw new KeyNotFoundException("Blog post not found.");
                }
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
                var posts = await GetAllAsync();
                var post = posts.FirstOrDefault(p => p.Id == id);
                if (post != null)
                {
                    posts.Remove(post);
                    await SaveAsync(posts);
                }
                else
                {
                    throw new KeyNotFoundException("Blog post not found.");
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while deleting the blog post.", ex);
            }
        }

        private async Task SaveAsync(List<BlogPost> posts)
        {
            try
            {
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(posts));
            }
            catch (IOException ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while saving the blog posts.", ex);
            }
        }
    }
}