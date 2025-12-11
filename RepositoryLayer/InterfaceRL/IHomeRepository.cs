using Levavishwam_Backend.DTOs.Home;

namespace Levavishwam_Backend.RepositoryLayer.InterfaceRL
{
    public interface IHomeRepository
    {
        Task<List<CarouselDto>> GetCarouselAsync();
        Task<List<EventDto>> GetEventsAsync();
        Task<EventDto?> GetEventByIdAsync(int id);

        Task<List<NewsDto>> GetNewsAsync();
        Task<NewsDto?> GetNewsByIdAsync(int id);
        Task<List<DownloadDto>> GetDownloadsAsync();
        Task<List<CommitteeMemberDto>> GetCommitteeAsync();
        Task<List<InformationDto>> GetInformationAsync();
    }
}
