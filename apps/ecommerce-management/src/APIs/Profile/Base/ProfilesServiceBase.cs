using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using EcommerceManagement.APIs.Extensions;
using EcommerceManagement.Infrastructure;
using EcommerceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManagement.APIs;

public abstract class ProfilesServiceBase : IProfilesService
{
    protected readonly EcommerceManagementDbContext _context;

    public ProfilesServiceBase(EcommerceManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Profile
    /// </summary>
    public async Task<Profile> CreateProfile(ProfileCreateInput createDto)
    {
        var profile = new ProfileDbModel
        {
            Bio = createDto.Bio,
            CreatedAt = createDto.CreatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            profile.Id = createDto.Id;
        }
        if (createDto.User != null)
        {
            profile.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProfileDbModel>(profile.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Profile
    /// </summary>
    public async Task DeleteProfile(ProfileWhereUniqueInput uniqueId)
    {
        var profile = await _context.Profiles.FindAsync(uniqueId.Id);
        if (profile == null)
        {
            throw new NotFoundException();
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Profiles
    /// </summary>
    public async Task<List<Profile>> Profiles(ProfileFindManyArgs findManyArgs)
    {
        var profiles = await _context
            .Profiles.Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return profiles.ConvertAll(profile => profile.ToDto());
    }

    /// <summary>
    /// Meta data about Profile records
    /// </summary>
    public async Task<MetadataDto> ProfilesMeta(ProfileFindManyArgs findManyArgs)
    {
        var count = await _context.Profiles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Profile
    /// </summary>
    public async Task<Profile> Profile(ProfileWhereUniqueInput uniqueId)
    {
        var profiles = await this.Profiles(
            new ProfileFindManyArgs { Where = new ProfileWhereInput { Id = uniqueId.Id } }
        );
        var profile = profiles.FirstOrDefault();
        if (profile == null)
        {
            throw new NotFoundException();
        }

        return profile;
    }

    /// <summary>
    /// Update one Profile
    /// </summary>
    public async Task UpdateProfile(ProfileWhereUniqueInput uniqueId, ProfileUpdateInput updateDto)
    {
        var profile = updateDto.ToModel(uniqueId);

        if (updateDto.User != null)
        {
            profile.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(profile).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Profiles.Any(e => e.Id == profile.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a User record for Profile
    /// </summary>
    public async Task<User> GetUser(ProfileWhereUniqueInput uniqueId)
    {
        var profile = await _context
            .Profiles.Where(profile => profile.Id == uniqueId.Id)
            .Include(profile => profile.User)
            .FirstOrDefaultAsync();
        if (profile == null)
        {
            throw new NotFoundException();
        }
        return profile.User.ToDto();
    }
}
