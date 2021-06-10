using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Gigs
{
    public interface IGigManager : IDomainService
    {
        Task<Gig> CreateAsync(string title, string coverImage,string description);
    }
}