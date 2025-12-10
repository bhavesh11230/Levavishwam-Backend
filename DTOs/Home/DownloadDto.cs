namespace Levavishwam_Backend.DTOs.Home
{
    public class DownloadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FileType { get; set; }
        public string? Size { get; set; }
        public string? UploadDate { get; set; }
        public int DownloadsCount { get; set; }
        public string? Category { get; set; }

        public string FileUrl { get; set; }
    }
}
