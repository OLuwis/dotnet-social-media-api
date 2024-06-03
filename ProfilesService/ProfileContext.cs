using Microsoft.EntityFrameworkCore;
using ProfilesService.Entities;

namespace ProfilesService;

public class ProfileContext : DbContext
{
    public ProfileContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ProfileEntity> Profiles { get; set; }
}