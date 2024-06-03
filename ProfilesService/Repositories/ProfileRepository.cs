using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Entities;
using APICommons;

namespace ProfilesService.Repositories;

public class ProfileRepository : IRepository<ProfileEntity>
{
    public ProfileContext context;

    public ProfileRepository(ProfileContext context)
    {
        this.context = context;
    }

    public async Task<ProfileEntity> GetBy(Expression<Func<ProfileEntity, bool>> predicate)
    {
        var profile = await context.Profiles.SingleAsync(predicate);
        return profile;
    }

    public async Task<ProfileEntity> GetById(Guid id)
    {
        var profile = await context.Profiles.SingleAsync(p => p.Id.Equals(id));
        return profile;
    }

    public async Task<List<ProfileEntity>> GetAll(int page)
    {
        var profiles = await context.Profiles
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();
        return profiles;
    }

    public async Task<List<ProfileEntity>> GetAllBy(Expression<Func<ProfileEntity, bool>> predicate, int page)
    {
        var profiles = await context.Profiles
            .Skip(page * 10)
            .Take(10)
            .Where(predicate)
            .ToListAsync();
        return profiles;
    }

    public async Task<ProfileEntity> Save(ProfileEntity entity)
    {
        await context.Profiles.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(ProfileEntity entity)
    {
        context.Profiles.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(ProfileEntity entity)
    {
        context.Profiles.Update(entity);
        await context.SaveChangesAsync();
    }
}