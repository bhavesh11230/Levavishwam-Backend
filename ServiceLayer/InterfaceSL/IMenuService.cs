using Levavishwam_Backend.Models;

namespace Levavishwam_Backend.ServiceLayer.InterfaceSL
{
    public interface IMenuService
    {
        Task<List<Menu>> GetAllMenus();
        Task<Menu> AddMenu(Menu menu);
        Task<Menu> UpdateMenu(Menu menu);
        Task<bool> DeleteMenu(int id);
    }
}
