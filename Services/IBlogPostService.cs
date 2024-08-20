using BlogManagementApi.Models;

namespace BlogManagementApi.Services
{
    public interface IBlogPostService
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost> GetAsync(int id);
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
        Task DeleteAsync(int id);
    }
}