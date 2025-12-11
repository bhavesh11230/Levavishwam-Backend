using Levavishwam_Backend.Models;

namespace Levavishwam_Backend.RepositoryLayer.InterfaceRL
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAll();
        Task<Menu> Add(Menu menu);
        Task<Menu> Update(Menu menu);
        Task<bool> Delete(int id);
    }
}
