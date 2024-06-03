using MassTransit;
using MessageContracts;
using Microsoft.EntityFrameworkCore;
using PostsService.Repositories;

namespace PostsService.Consumers;

public class ProfileDeletedConsumer : IConsumer<ProfileDeleted>
{
    public ProfilesRepository profilesRepo;

    public ProfileDeletedConsumer(ProfilesRepository profilesRepo)
    {
        this.profilesRepo = profilesRepo;
    }

    public async Task Consume(ConsumeContext<ProfileDeleted> context)
    {
        var profile = await profilesRepo.GetById(context.Message.Id);
        await profilesRepo.Delete(profile);
    }
}