using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostsService.Entities;
using APICommons;

namespace PostsService.Repositories;

public class PostsRepository : IRepository<PostsEntity>
{
    private readonly PostsContext context;

    public PostsRepository(PostsContext context)
    {
        this.context = context;
    }

    public async Task<PostsEntity> GetById(Guid id)
    {
        var post = await context.Posts.Include(post => post.Profile).SingleAsync(p => p.Id.Equals(id));
        return post;
    }

    public async Task<PostsEntity> GetBy(Expression<Func<PostsEntity, bool>> predicate)
    {
        var post = await context.Posts.SingleAsync(predicate);
        return post;
    }

    public async Task<List<PostsEntity>> GetAllBy(Expression<Func<PostsEntity, bool>> predicate, int page)
    {
        var posts = await context.Posts
            .Skip(page * 10)
            .Take(10)
            .Where(predicate)
            .ToListAsync();
        return posts;
    }

    public async Task<List<PostsEntity>> GetAll(int page)
    {
        var posts = await context.Posts
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();
        return posts;
    }

    public async Task<PostsEntity> Save(PostsEntity entity)
    {
        await context.Posts.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(PostsEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(PostsEntity entity)
    {
        context.Posts.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<ProfilesEntity> GetProfileById(Guid id)
    {
        var profile = await context.Profiles.SingleAsync(p => p.Id.Equals(id));
        return profile;
    }
}