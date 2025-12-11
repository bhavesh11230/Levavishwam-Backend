using Levavishwam_Backend.Models;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;

namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;

        public MenuService(IMenuRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Menu>> GetAllMenus()
        {
            return _repo.GetAll();
        }

        public Task<Menu> AddMenu(Menu menu)
        {
            return _repo.Add(menu);
        }

        public Task<Menu> UpdateMenu(Menu menu)
        {
            return _repo.Update(menu);
        }

        public Task<bool> DeleteMenu(int id)
        {
            return _repo.Delete(id);
        }
    }
}
