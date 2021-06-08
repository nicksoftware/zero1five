using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Gigs
{
    public class GigManager : DomainService, IDomainService
    {
        Task<Gig> CreateAsync(string title, string description)
        {
            throw new NotImplementedException();
        }
    }
}