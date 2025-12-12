// UpdateProfileRequest.cs
namespace Levavishwam_Backend.DTOs.Profile
{
    public class UpdateProfileRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string CommunityInfo { get; set; }
        public string? ProfilePhotoPath { get; set; }
    }
}
