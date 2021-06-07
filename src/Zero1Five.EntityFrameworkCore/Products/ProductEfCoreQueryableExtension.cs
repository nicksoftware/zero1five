using System;
using System.Linq;

namespace Zero1Five.Products
{
    public static class ProductEfCoreQueryableExtension
    {
        public static IQueryable<Product> IncludeDetails(this IQueryable<Product> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) 
                ;
        }
    }
}