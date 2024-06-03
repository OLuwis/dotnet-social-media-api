using MassTransit;
using MessageContracts;
using PostsService.Repositories;

namespace PostsService.Consumers;

public class ProfileUpdatedConsumer : IConsumer<ProfileUpdated>
{
    public ProfilesRepository profilesRepo;

    public ProfileUpdatedConsumer(ProfilesRepository profilesRepo)
    {
        this.profilesRepo = profilesRepo;
    }

    public async Task Consume(ConsumeContext<ProfileUpdated> context)
    {
        var profile = await profilesRepo.GetById(context.Message.Id);

        profile.Name = context.Message.Name;
        profile.Username = context.Message.Username;

        await profilesRepo.Update(profile);
    }
}