using BlazorApp1.Dto;

namespace BlazorApp1.Services.Interfaces
{
    /// <summary>
    /// Service that provides dummy data to display
    /// </summary>
    public interface IDummyApiService
    {
        Task<UsersRoot?> GetUsersAsync();
        Task<PostsRoot?> GetUserPostsAsync(User user);
    }
}
