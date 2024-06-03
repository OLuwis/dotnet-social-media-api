using APICommons;

namespace ProfilesService.Entities;

public class ProfileEntity : IEntity
{
    public ProfileEntity(Guid id, string name, string username, string country, DateOnly birthday, string biography)
    {
        Id = id;
        Name = name;
        Username = username;
        Country = country;
        Birthday = birthday;
        Biography = biography;
    }

    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Country { get; set; }
    public DateOnly Birthday { get; set; }
    public string Biography { get; set; }
}