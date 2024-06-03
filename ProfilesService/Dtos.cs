namespace ProfilesService;

public record ProfileDTO(Guid Id, string Name, string Username, string Country, DateOnly Birthday, string Biography);
public record CreateProfileDTO(string Name, string Username, string Country, DateOnly Birthday, string Biography);
public record UpdateProfileDTO(string Name, string Username, string Country, DateOnly Birthday, string Biography);