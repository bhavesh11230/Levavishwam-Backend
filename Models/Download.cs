namespace Levavishwam_Backend.Models
{
    public class Download
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FileType { get; set; }
        public string? Size { get; set; }
        public string? UploadDate { get; set; }
        public int DownloadsCount { get; set; }
        public string? Category { get; set; }

        public string? FileUrl { get; set; }

    }

}
