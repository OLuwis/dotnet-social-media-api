using PostsService.Dtos;
using PostsService.Entities;

namespace PostsService;

public static class Extensions
{
    public static PostDTO MapToDTO(this PostsEntity post)
    {
        return new PostDTO(post.Id, post.Profile.MapToDTO(), post.Content, post.CreatedAt);
    }

    public static ProfileDTO MapToDTO(this ProfilesEntity profile)
    {
        return new ProfileDTO(profile.Id, profile.Name, profile.Username);
    }
}