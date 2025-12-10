namespace Levavishwam_Backend.DTOs.Home
{
    public class EventDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EventDate { get; set; }
        public string? EventTime { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Status { get; set; }
    }
}
