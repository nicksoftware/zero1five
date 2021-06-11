using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Shouldly;
using Volo.Abp.Json.SystemTextJson;
using Xunit;
using Zero1Five.Categories;
using Zero1Five.TestBase;

namespace Zero1Five.Products
{
    public class ProductManagerTests : Zero1FiveDomainTestBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;
        private readonly ICategoryRepository _categoryRepository;
        public ProductManagerTests()
        {
            _productRepository = GetRequiredService<IProductRepository>();
            _productManager = GetRequiredService<IProductManager>();
            _categoryRepository = GetRequiredService<ICategoryRepository>();

        }

        [Fact]
        public async Task Create_Should_Create_And_Return_NewProduct()
        {
            //Given
            var id = Guid.NewGuid();

            var category = (await _categoryRepository.GetListAsync()).First();
            var title = "New Product";
            var description = "New Product";
            var cover = "coverImage.jpg";
            var productManger = new ProductManager(_productRepository);
            Product result = null;

            await WithUnitOfWorkAsync(async () =>
            {
                var gigId = Guid.Parse(Zero1FiveTestData.GigId);
                
                result = await _productManager.CreateAsync(title, gigId, category.Id, cover, description);
                result.ShouldNotBeNull();
                result.Id.ShouldNotBe(Guid.Empty);
                result.CategoryId.ShouldBe(category.Id);
                result.Title.ShouldBe(title);
                result.CoverImage.ShouldBe(cover);
                result.Description.ShouldBe(description);
            });

        }

        [Fact]
        public async  Task ChangeCoverImageAsync_ShouldChangeImage()
        {
            var product = (await _productRepository.GetListAsync()).First();
            var coverImage = "newImage.jpg";
            var result = await _productManager.ChangeCoverImageAsync(product, coverImage);
            result.CoverImage.ShouldBe(coverImage);
        }
    }
}