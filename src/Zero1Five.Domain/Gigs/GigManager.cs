using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Gigs
{
    public class GigManager : DomainService, IGigManager
    {
        private readonly IGigRepository _gigRepository;

        public GigManager(IGigRepository gigRepository)
        {
            _gigRepository = gigRepository;
        }

        public Task<Gig> CreateAsync(string title, string coverImage, string description)
        {
            var newGig = Gig.Create(GuidGenerator.Create(), title, coverImage, description);
            return _gigRepository.InsertAsync(newGig, true);
        }
    }
}