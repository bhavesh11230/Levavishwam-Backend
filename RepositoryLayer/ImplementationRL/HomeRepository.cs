using Levavishwam_Backend.Data;
using Levavishwam_Backend.DTOs.Home;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Microsoft.EntityFrameworkCore;

namespace Levavishwam_Backend.RepositoryLayer.ImplementationRL
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDbContext _db;

        public HomeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<CarouselDto>> GetCarouselAsync() =>
            await _db.Carousel.Select(x => new CarouselDto
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                Description = x.Description,
                CtaText = x.CtaText,
                CtaLink = x.CtaLink
            }).ToListAsync();

        public async Task<List<EventDto>> GetEventsAsync() =>
            await _db.Events.Select(x => new EventDto
            {
                Id = x.Id,
                Title = x.Title,
                EventDate = x.EventDate,
                EventTime = x.EventTime,
                Location = x.Location,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Status = x.Status
            }).ToListAsync();

        public async Task<List<NewsDto>> GetNewsAsync() =>
            await _db.News.Select(x => new NewsDto
            {
                Id = x.Id,
                Title = x.Title,
                Excerpt = x.Excerpt,
                ImageUrl = x.ImageUrl,
                NewsDate = x.NewsDate,
                Author = x.Author,
                Category = x.Category,
                 Content = x.Content
            }).ToListAsync();

        
        public async Task<NewsDto?> GetNewsByIdAsync(int id)
        {
            return await _db.News
                .Where(x => x.Id == id)
                .Select(x => new NewsDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Excerpt = x.Excerpt,
                    ImageUrl = x.ImageUrl,
                    NewsDate = x.NewsDate,
                    Author = x.Author,
                    Category = x.Category,
                    Content = x.Content
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<DownloadDto>> GetDownloadsAsync() =>
            await _db.Downloads.Select(x => new DownloadDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                FileType = x.FileType,
                Size = x.Size,
                UploadDate = x.UploadDate,
                DownloadsCount = x.DownloadsCount,
                Category = x.Category,
                FileUrl = x.FileUrl
            }).ToListAsync();

        public async Task<List<CommitteeMemberDto>> GetCommitteeAsync() =>
            await _db.Committee.Select(x => new CommitteeMemberDto
            {
                Id = x.Id,
                Name = x.Name,
                Role = x.Role,
                Department = x.Department,
                ImageUrl = x.ImageUrl,
                Email = x.Email,
                Phone = x.Phone,
                Bio = x.Bio,
                JoinYear = x.JoinYear
            }).ToListAsync();

        public async Task<List<InformationDto>> GetInformationAsync() =>
            await _db.Information.Select(x => new InformationDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();
    }
}
