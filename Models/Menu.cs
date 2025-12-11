namespace Levavishwam_Backend.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsAdminOnly { get; set; } = false;

    }
}