using System;
using Volo.Abp.Domain.Repositories;

namespace Zero1Five.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}