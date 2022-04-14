using BlazorApp1.Authentication;
using BlazorApp1.Services.Interfaces;

namespace BlazorApp1.Services
{
    public class UserAccountService : IUserAccountService
    {
        private List<UserAccount> _users = new()
        {
            new UserAccount("test", "test@test.com", "test", UserAccountRoles.ADMIN),
            new UserAccount("test2", "test2@test.com", "test", UserAccountRoles.USER),
        };

        public UserAccount? FindUserAccount(string email, string password) => 
            _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);
    }
}
