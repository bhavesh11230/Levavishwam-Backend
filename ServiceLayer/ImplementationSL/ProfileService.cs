using Levavishwam_Backend.Data;
using Levavishwam_Backend.DTOs.Profile;
using Levavishwam_Backend.Models;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _db;
        private readonly IProfileRepository _repo;
        private readonly IUploadService _uploadService;

        public ProfileService(AppDbContext db, IProfileRepository repo, IUploadService uploadService)
        {
            _db = db;
            _repo = repo;
            _uploadService = uploadService;
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
                ProfilePhotoPath = profile?.ProfilePhotoPath,
                Status = user?.Status,
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

        public async Task<string> UpdateProfilePhotoAsync(int userId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadedPath = await _uploadService.UploadFileAsync(file, "profile-photos");
            if (string.IsNullOrWhiteSpace(uploadedPath)) return null;

            var (user, profile) = await _repo.GetByUserIdAsync(userId);
            if (user == null)
            {
                await _uploadService.DeleteFileAsync(uploadedPath);
                return null;
            }

            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };
            }

            var oldPath = profile.ProfilePhotoPath;
            profile.ProfilePhotoPath = uploadedPath;
            profile.UpdatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(user, profile);
            try
            {
                await _repo.SaveChangesAsync();
            }
            catch
            {
                await _uploadService.DeleteFileAsync(uploadedPath);
                throw; 
            }

            if (!string.IsNullOrWhiteSpace(oldPath))
            {
                await _uploadService.DeleteFileAsync(oldPath);
            }

            return uploadedPath;
        }

        public async Task<bool> DeleteProfilePhotoAsync(int userId)
        {
            var (user, profile) = await _repo.GetByUserIdAsync(userId);
            if (profile == null) return false;

            var currentPath = profile.ProfilePhotoPath;
            if (!string.IsNullOrWhiteSpace(currentPath))
            {
                await _uploadService.DeleteFileAsync(currentPath);
            }

            profile.ProfilePhotoPath = null;
            profile.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(user, profile);
            await _repo.SaveChangesAsync();

            return true;
        }

    }
}
