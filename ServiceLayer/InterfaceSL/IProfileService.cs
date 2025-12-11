using Levavishwam_Backend.DTOs.Profile;

namespace Levavishwam_Backend.ServiceLayer.InterfaceSL
{
    public interface IProfileService
    {
        Task<GetProfileResponseDto> GetProfileAsync(int userId);
        Task<bool> UpdateProfileAsync(int userId, UpdateProfileRequestDto dto);
    }
}
