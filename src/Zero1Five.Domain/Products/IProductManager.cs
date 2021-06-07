using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Products
{
    internal interface IProductManager : IDomainService
    {
        Task<Product> CreateAsync(string title, Guid categoryId, string cover);
    }
}