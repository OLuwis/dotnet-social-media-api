using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using PostsService.Entities;

namespace PostsService;

public class PostsContext : DbContext
{
    public PostsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PostsEntity> Posts { get; set; }
    public DbSet<ProfilesEntity> Profiles { get; set; }
}