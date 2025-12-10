using Levavishwam_Backend.DTOs.Home;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;

namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _repo;

        public HomeService(IHomeRepository repo)
        {
            _repo = repo;
        }

        public Task<List<CarouselDto>> GetCarouselAsync() => _repo.GetCarouselAsync();
        public Task<List<EventDto>> GetEventsAsync() => _repo.GetEventsAsync();
        public Task<List<NewsDto>> GetNewsAsync() => _repo.GetNewsAsync();
        public Task<List<DownloadDto>> GetDownloadsAsync() => _repo.GetDownloadsAsync();
        public Task<List<CommitteeMemberDto>> GetCommitteeAsync() => _repo.GetCommitteeAsync();
        public Task<List<InformationDto>> GetInformationAsync() => _repo.GetInformationAsync();
        public Task<NewsDto?> GetNewsByIdAsync(int id) => _repo.GetNewsByIdAsync(id);
    }
}
