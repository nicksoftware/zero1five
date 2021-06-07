using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zero1Five.EntityFrameworkCore;

namespace Zero1Five.Products
{
    public class ProductRepository : EfCoreRepository<Zero1FiveDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<Zero1FiveDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

    }
}