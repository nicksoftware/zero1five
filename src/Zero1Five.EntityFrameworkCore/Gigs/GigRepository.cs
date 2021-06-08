using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zero1Five.EntityFrameworkCore;

namespace Zero1Five.Gigs
{
    public class GigRepository : EfCoreRepository<Zero1FiveDbContext, Gig, Guid>, IGigRepository
    {
        public GigRepository(IDbContextProvider<Zero1FiveDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}