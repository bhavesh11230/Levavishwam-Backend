using Levavishwam_Backend.Data;
using Levavishwam_Backend.Models;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Microsoft.EntityFrameworkCore;

namespace Levavishwam_Backend.RepositoryLayer.ImplementationRL
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _db;
        public ProfileRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<(User user, UserProfile profile)> GetByUserIdAsync(int userId)
        {
            var user = await _db.Users.Include(u => u.UserProfile).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return (null, null);
            return (user, user.UserProfile);
        }
        public async Task UpdateAsync(User user, UserProfile profile)
        {
            _db.Users.Update(user);
            if (profile != null)
            {
                _db.UserProfile.Update(profile);
            }
            await Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
