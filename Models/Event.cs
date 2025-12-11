namespace Levavishwam_Backend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EventDate { get; set; }
        public string? EventTime { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Status { get; set; }

        public string? Content { get; set; }
    }
}
