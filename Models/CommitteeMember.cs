namespace Levavishwam_Backend.Models
{
    public class CommitteeMember
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Department { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public int? JoinYear { get; set; }
    }
}
