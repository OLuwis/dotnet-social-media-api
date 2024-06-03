using APICommons;

namespace PostsService.Entities;

public class PostsEntity : IEntity
{
    public PostsEntity(Guid id)
    {
        Id = id;
    }

    public PostsEntity(Guid id, ProfilesEntity profile, string content, DateTimeOffset createdAt)
    {
        Id = id;
        Content = content;
        Profile = profile;
        CreatedAt = createdAt;
    }

    public Guid Id { get; init; }
    public ProfilesEntity Profile { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}