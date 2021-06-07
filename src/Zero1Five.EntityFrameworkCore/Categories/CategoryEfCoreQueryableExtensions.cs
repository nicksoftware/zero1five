using System;
using System.Linq;
using Zero1Five.Categories;

namespace Zero1Five.EntityFrameworkCore.Categories
{
    public static class CategoryEfCoreQueryableExtensions
    {
        public static IQueryable<Category> IncludeDetails(this IQueryable<Category> queryable, bool include = true)
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