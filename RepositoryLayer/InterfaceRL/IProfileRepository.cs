using Levavishwam_Backend.Models;

namespace Levavishwam_Backend.RepositoryLayer.InterfaceRL
{
    public interface IProfileRepository
    {
        Task<(User user, UserProfile profile)> GetByUserIdAsync(int userId);
        Task UpdateAsync(User user, UserProfile profile);
        Task SaveChangesAsync();
    }
}
