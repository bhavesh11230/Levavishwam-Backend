using Levavishwam_Backend.Data;
using Levavishwam_Backend.Models;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;

using Microsoft.EntityFrameworkCore;

namespace Levavishwam_Backend.RepositoryLayer.ImplementationRL
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetAll()
        {
            return await _context.Menus.OrderBy(m => m.OrderNo).ToListAsync();
        }

        public async Task<Menu> Add(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> Update(Menu menu)
        {
            var existing = await _context.Menus.FindAsync(menu.Id);

            if (existing == null)
                throw new Exception("Menu not found");

            existing.Title = menu.Title;
            existing.Path = menu.Path;
            existing.OrderNo = menu.OrderNo;
            existing.IsActive = menu.IsActive;
            existing.IsAdminOnly = menu.IsAdminOnly;

            await _context.SaveChangesAsync();
            return existing;
        }


        public async Task<bool> Delete(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
                return false;

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
