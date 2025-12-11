using Levavishwam_Backend.DTOs.Profile;
using Levavishwam_Backend.Models;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;

namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repo;
        public ProfileService(IProfileRepository repo)
        {
            _repo = repo;
        }
        public async Task<GetProfileResponseDto> GetProfileAsync(int userId)
        {
            var (user, profile) = await _repo.GetByUserIdAsync(userId);
            if (user == null) return null;
            var dto = new GetProfileResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Mobile = profile?.Mobile,
                Address = profile?.Address,
                DOB = profile?.DOB,
                Gender = profile?.Gender,
                CommunityInfo = profile?.CommunityInfo,
                ProfilePhotoPath = profile?.ProfilePhotoPath
            };
            return dto;
        }
        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileRequestDto dto)
        {
            var (user, profile) = await _repo.GetByUserIdAsync(userId);
            if (user == null) return false;
            user.Name = dto.Name ?? user.Name;
            user.Email = dto.Email ?? user.Email;
            user.UpdatedAt = DateTime.UtcNow;
            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };
            }
            profile.Mobile = dto.Mobile ?? profile.Mobile;
            profile.Address = dto.Address ?? profile.Address;
            profile.DOB = dto.DOB ?? profile.DOB;
            profile.Gender = dto.Gender ?? profile.Gender;
            profile.CommunityInfo = dto.CommunityInfo ?? profile.CommunityInfo;
            profile.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(user, profile);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
