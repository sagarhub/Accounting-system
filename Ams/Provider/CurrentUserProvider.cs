using System.Security.Claims;
using Ams.Models;
using Ams.Provider.Interfaces;
using Ams.Data;

namespace Ams.Provider;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppDbContext _context;

    public CurrentUserProvider(IHttpContextAccessor contextAccessor, AppDbContext context)
    {
        _contextAccessor = contextAccessor;
        _context = context;
    }

    public bool IsLoggedIn()
        => GetCurrentUserId() != null;

    public async Task<User?> GetCurrentUser()
    {
        var currentUserId = GetCurrentUserId();
        if (!currentUserId.HasValue) return null;

        return await _context.users.FindAsync(currentUserId.Value);
    }

    public long? GetCurrentUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue("Id");
        if (string.IsNullOrWhiteSpace(userId)) return null;
        return Convert.ToInt64(userId);
    }
}