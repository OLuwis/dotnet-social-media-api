using System.ComponentModel.DataAnnotations;
using MassTransit;
using MessageContracts;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Entities;
using ProfilesService.Repositories;

namespace ProfilesService.Controllers;

[ApiController]
[Route("profiles")]
public class ProfileController : ControllerBase
{
    public ProfileRepository profileRepository;
    public IPublishEndpoint publisher;

    public ProfileController(ProfileRepository profileRepository, IPublishEndpoint publisher)
    {
        this.profileRepository = profileRepository;
        this.publisher = publisher;
    }

    [HttpGet("{id}")]
    public async Task<ProfileDTO> GetProfileById(Guid id)
    {
        var profile = await profileRepository.GetById(id);
        return new ProfileDTO(profile.Id, profile.Name, profile.Username, profile.Country, profile.Birthday, profile.Biography);
    }

    [HttpGet("/profiles/recent")]
    public async Task<IEnumerable<ProfileDTO>> GetRecentProfiles([Required][FromQuery] int page)
    {
        var profiles = await profileRepository.GetAll(page);
        return profiles.ConvertAll(p => new ProfileDTO(p.Id, p.Name, p.Username, p.Country, p.Birthday, p.Biography));
    }

    [HttpPost]
    public async Task<ProfileDTO> CreateProfile([FromBody] CreateProfileDTO body)
    {
        var profile = new ProfileEntity(Guid.NewGuid(), body.Name, body.Username, body.Country, body.Birthday, body.Biography);
        await profileRepository.Save(profile);

        var message = new ProfileCreated(profile.Id, profile.Name, profile.Username);
        await publisher.Publish(message);

        return new ProfileDTO(profile.Id, profile.Name, profile.Username, profile.Country, profile.Birthday, profile.Biography);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileDTO body)
    {
        var profile = await profileRepository.GetById(id);

        profile.Name = body.Name;
        profile.Username = body.Username;
        profile.Country = body.Country;
        profile.Birthday = body.Birthday;
        profile.Biography = body.Biography;

        await profileRepository.Update(profile);

        var message = new ProfileUpdated(profile.Id, profile.Name, profile.Username);
        await publisher.Publish(message);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(Guid id)
    {
        var profile = await profileRepository.GetById(id);
        await profileRepository.Delete(profile);

        var message = new ProfileDeleted(profile.Id);
        await publisher.Publish(message);

        return NoContent();
    }
}