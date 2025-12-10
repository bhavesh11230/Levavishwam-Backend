namespace Levavishwam_Backend.DTOs.Home
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Excerpt { get; set; }
        public string? ImageUrl { get; set; }
        public string? NewsDate { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }

        public string? Content { get; set; }

    }
}
