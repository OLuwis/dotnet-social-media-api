using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PostsService.Dtos;
using PostsService.Entities;
using PostsService.Repositories;

namespace PostsService.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    public PostsRepository postsRepository;
    public ProfilesRepository profilesRepository;

    public PostsController(PostsRepository postsRepository, ProfilesRepository profilesRepository)
    {
        this.postsRepository = postsRepository;
        this.profilesRepository = profilesRepository;
    }

    [HttpGet("{id}")]
    public async Task<PostDTO> GetPostById(Guid id)
    {
        var post = await postsRepository.GetById(id);
        Console.WriteLine(post.ToString());
        return post.MapToDTO();
    }

    [HttpGet("/posts/profile/{id}")]
    public async Task<IEnumerable<PostDTO>> GetPostsByProfileId(long id, [FromQuery(Name = "page")][Required] int page)
    {
        var posts = await postsRepository.GetAllBy(p => p.Profile.Id.Equals(id), page);
        return posts.ConvertAll(p => p.MapToDTO());
    }

    [HttpGet("/posts/recent")]
    public async Task<IEnumerable<PostDTO>> GetRecentPosts([FromQuery(Name = "page")][Required] int page)
    {
        var posts = await postsRepository.GetAll(page);
        return posts.ConvertAll(p => p.MapToDTO());
    }

    [HttpPost]
    public async Task<PostDTO> CreatePost([FromBody] CreatePostDTO body)
    {
        var profile = await profilesRepository.GetById(body.ProfileId);

        var newPost = new PostsEntity(Guid.NewGuid(), profile, body.Content, DateTimeOffset.UtcNow);

        return (await postsRepository.Save(newPost)).MapToDTO();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] string content)
    {
        var post = await postsRepository.GetById(id);
        post.Content = content;
        await postsRepository.Update(post);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var post = await postsRepository.GetById(id);
        await postsRepository.Delete(post);
        return NoContent();
    }
}