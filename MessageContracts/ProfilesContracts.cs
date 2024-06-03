namespace MessageContracts;

public record ProfileCreated(Guid Id, string Name, string Username);
public record ProfileUpdated(Guid Id, string Name, string Username);
public record ProfileDeleted(Guid Id);