using Ams.Models;

namespace Ams.Provider.Interfaces;

public interface ICurrentUserProvider
{
    bool IsLoggedIn();
    Task<User?> GetCurrentUser();
    long? GetCurrentUserId();
}