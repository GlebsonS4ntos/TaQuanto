using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaQuanto.Domain.Entities;

namespace TaQuanto.Infra.Data
{
    public class TaQuantoContext : IdentityDbContext<User>
    {
        public TaQuantoContext(DbContextOptions<TaQuantoContext> options) : base(options)
        {
        }
    }
}