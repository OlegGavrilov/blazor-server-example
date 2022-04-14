using BlazorApp1.Dto;

namespace BlazorApp1.Pages
{


    public partial class FetchData
    {
        private DataStateHolder<UsersRoot> usersRoot = new();
        private DataStateHolder<PostsRoot> postsRoot = new();

        protected override async Task OnInitializedAsync()
        {
            var loadingTask = async () => await dummyApiService.GetUsersAsync();

            await usersRoot.LoadDataAsync(loadingTask);
        }

        private async Task SelectUser(User user)
        {
            var loadingTask = async () => await dummyApiService.GetUserPostsAsync(user);

            await postsRoot.LoadDataAsync(loadingTask);
        }
    }
}