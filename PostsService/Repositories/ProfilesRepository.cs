using System.Linq.Expressions;
using APICommons;
using Microsoft.EntityFrameworkCore;
using PostsService.Entities;

namespace PostsService.Repositories;

public class ProfilesRepository : IRepository<ProfilesEntity>
{
    public PostsContext context;

    public ProfilesRepository(PostsContext context)
    {
        this.context = context;
    }

    public async Task<ProfilesEntity> GetBy(Expression<Func<ProfilesEntity, bool>> predicate)
    {
        var profile = await context.Profiles.SingleAsync(predicate);
        return profile;
    }

    public async Task<ProfilesEntity> GetById(Guid id)
    {
        var profile = await context.Profiles.SingleAsync(p => p.Id.Equals(id));
        return profile;
    }

    public async Task<List<ProfilesEntity>> GetAll(int page)
    {
        var profiles = await context.Profiles
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();
        return profiles;
    }

    public async Task<List<ProfilesEntity>> GetAllBy(Expression<Func<ProfilesEntity, bool>> predicate, int page)
    {
        var profiles = await context.Profiles
            .Skip(page * 10)
            .Take(10)
            .Where(predicate)
            .ToListAsync();
        return profiles;
    }

    public async Task<ProfilesEntity> Save(ProfilesEntity entity)
    {
        await context.Profiles.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(ProfilesEntity entity)
    {
        context.Profiles.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(ProfilesEntity entity)
    {
        context.Profiles.Update(entity);
        await context.SaveChangesAsync();
    }
}