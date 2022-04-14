using BlazorApp1.Dto;
using BlazorApp1.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using BlazorApp1.Services.Interfaces;

namespace BlazorApp1.Services
{
    /// <summary>
    /// Implementation of dummy data service provider
    /// Uses https://dummyapi.io/docs to query data
    /// Uses IMemoryCache to cache service responses
    /// </summary>
    public class DummyApiService: IDummyApiService
    {
        private readonly IMemoryCache _memoryCache;

        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        public HttpClient Client { get; }

        public DummyApiService(HttpClient client, IMemoryCache memoryCache, IOptions<DummyApiOptions> options)
        {
            client.BaseAddress = new Uri(options.Value.BaseUrl);

            client.DefaultRequestHeaders.Add("app-id",
                options.Value.Key);

            Client = client;

            _memoryCache = memoryCache;
        }

        public Task<UsersRoot?> GetUsersAsync()
        {
            var url = "data/v1/user";

            return _memoryCache.GetOrCreateAsync(url, async cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                var response = await Client.GetAsync("data/v1/user");

                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<UsersRoot>(responseStream);
            });
        }

        public Task<PostsRoot?> GetUserPostsAsync(User user)
        {
            var url = $"data/v1/user/{user.id}/post";

            return _memoryCache.GetOrCreateAsync(url, async cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                var response = await Client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<PostsRoot>(responseStream);
            });
        }
    }
}
