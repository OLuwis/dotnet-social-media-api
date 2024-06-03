using APICommons;

namespace PostsService.Entities;

public class ProfilesEntity : IEntity
{
    public ProfilesEntity(Guid id)
    {
        Id = id;
    }

    public ProfilesEntity(Guid id, string name, string username)
    {
        Id = id;
        Name = name;
        Username = username;
    }

    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Username { get; set; }
}