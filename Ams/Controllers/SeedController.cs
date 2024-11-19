using Ams.Data;
using Ams.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly AppDbContext context;

        public SeedController(AppDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet("/seed/admin")]
        public async Task<string> SeedAdminUser()
        {
            if(await context.users.AnyAsync())
            {
                return "Super admin already seeded";
            }
            var user = new User
            {
                Name = "Super",
                Last_name = "admin",
                Email = "super.admin",
                Contact = 12,
                address = "",
                UserName = "super.admin",
                Password = "admin"
            };
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            context.users.Add(user);
            await context.SaveChangesAsync();
            return "Admin user seeding complete";
        }
        
    }
}
