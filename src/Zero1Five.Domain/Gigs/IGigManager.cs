using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Gigs
{
    public interface IGigManager : IDomainService
    {
        Task<Gig> CreateAsync(string title,Guid categoryId, string coverImage,string description);
    }
}