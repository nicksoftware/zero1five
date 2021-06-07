using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zero1Five.EntityFrameworkCore;
using Zero1Five.Categories;

namespace Zero1Five.Categories
{
    public class CategoryRepository : EfCoreRepository<Zero1FiveDbContext, Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<Zero1FiveDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}