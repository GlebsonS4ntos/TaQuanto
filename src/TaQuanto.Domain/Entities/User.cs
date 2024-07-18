using Microsoft.AspNetCore.Identity;

namespace TaQuanto.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? ValidToRefreshToken { get; set; }
    }
}
