namespace PostsService.Dtos;

public record PostDTO(Guid Id, ProfileDTO Profile, string Content, DateTimeOffset CreatedAt);
public record CreatePostDTO(Guid ProfileId, string Content);