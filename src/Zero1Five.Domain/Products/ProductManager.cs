using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace Zero1Five.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> CreateAsync(string title, Guid gigId, Guid categoryId, string cover, string description)
        {

            var id = GuidGenerator.Create();
            
            var product = new ProductBuilder(id)
            .WithGigId(gigId)
            .WithCategoryId(categoryId)
            .WithCoverImage(cover)
            .WithTitle(title)
            .WithDescription(description)
            .Build();

            return _productRepository.InsertAsync(product, true);
        }

        public async Task<Guid> PublishAsync(Product product)
        {
            if (product.IsPublished)
                throw new ProductAlreadyPublishedException(product.Title);
            product.Publish();
            return (await _productRepository.UpdateAsync(product)).Id;
        }

        public async Task<Guid> UnPublishAsync(Product product)
        {
            if (!product.IsPublished)
                throw new ProductAlreadyUnpublishedException(product.Title);

            product.Unpublish();
            return (await _productRepository.UpdateAsync(product)).Id;
        }

        public Task<Product> ChangeCoverImageAsync(Product product, string coverImage)
        {
            product.SetCover(coverImage);
            return Task.FromResult(product);
        }
    }
}