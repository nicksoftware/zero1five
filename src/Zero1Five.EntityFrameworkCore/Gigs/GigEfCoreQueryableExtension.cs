using System;
using System.Linq;

namespace Zero1Five.Gigs
{
    public static class GigEfCoreQueryableExtension
    {

        public static IQueryable<Gig> IncludeDetails(this IQueryable<Gig> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: 
                ;
        }
    }
}