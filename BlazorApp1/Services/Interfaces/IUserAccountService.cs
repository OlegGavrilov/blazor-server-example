using BlazorApp1.Authentication;

namespace BlazorApp1.Services.Interfaces
{
    public interface IUserAccountService
    {
        public UserAccount? FindUserAccount(string email, string password);
    }
}
