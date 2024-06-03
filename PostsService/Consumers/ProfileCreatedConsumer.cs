using MassTransit;
using MessageContracts;
using PostsService.Entities;
using PostsService.Repositories;

namespace PostsService.Consumers;

public class ProfileCreatedConsumer : IConsumer<ProfileCreated>
{
    public ProfilesRepository profilesRepo;

    public ProfileCreatedConsumer(ProfilesRepository profilesRepo)
    {
        this.profilesRepo = profilesRepo;
    }

    public async Task Consume(ConsumeContext<ProfileCreated> context)
    {
        var profile = new ProfilesEntity(context.Message.Id);

        profile.Name = context.Message.Name;
        profile.Username = context.Message.Username;

        await profilesRepo.Save(profile);
    }
}