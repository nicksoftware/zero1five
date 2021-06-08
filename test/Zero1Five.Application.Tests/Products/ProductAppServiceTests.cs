using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Zero1Five.Categories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zero1Five.TestBase;

namespace Zero1Five.Products
{
    public class ProductAppServiceTests : Zero1FiveApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductAppServiceTests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
            _categoryRepository = GetRequiredService<ICategoryRepository>();
            _productRepository = GetRequiredService<IProductRepository>();
        }
        [Fact]
        public async Task GetLookUpCategories_Should_GetCategories()
        {
            var result = await _productAppService.GetLookUpCategories();
            result.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateAsync_Should_UnPublishedProduct()
        {
            ProductDto result = null;
            //Given
            var input = new CreateProductDto
            {
                Title = "New Product",
                Description = "New Product Description",
                CoverImage = "demo.jpg",
                GigId = Guid.Parse(Zero1FiveTestData.GigId),
                IsPublished = false
            };
            await WithUnitOfWorkAsync(async () =>
                {
                    var categories = await _categoryRepository.GetListAsync();

                    input.CategoryId = categories.First().Id;
                    //When
                    result = await _productAppService.CreateAsync(input);
                });
            //Then
            result.Id.ShouldNotBe(Guid.Empty);
            result.CategoryId.ShouldNotBe(Guid.Empty);
            result.Title.ShouldBe(input.Title);
            result.Description.ShouldBe(input.Description);
            result.CoverImage.ShouldBe(input.CoverImage);
            result.IsPublished.ShouldBe(false);
        }

        [Fact]
        public async Task Publish_ShouldPublish()
        {
            //Given
            Product uPublishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                uPublishedProduct = await _productRepository.FirstOrDefaultAsync(x => !x.IsPublished);
            });

            //When
            var result = await _productAppService.PublishAsync(uPublishedProduct.Id);

            Product publishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                publishedProduct = await _productRepository.FindAsync(uPublishedProduct.Id);
            });
            //Than
            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(uPublishedProduct.Id);
            publishedProduct.ShouldNotBeNull();
            publishedProduct.IsPublished.ShouldBe(true);

        }

        [Fact]
        public async Task UnPublish_ShouldUnPublish()
        {
            //Given
            Product uPublishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                uPublishedProduct = await _productRepository.FirstOrDefaultAsync(x => x.IsPublished);
            });

            //When
            var result = await _productAppService.UnPublishAsync(uPublishedProduct.Id);

            Product publishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                publishedProduct = await _productRepository.FindAsync(uPublishedProduct.Id);
            });
            //Than
            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(uPublishedProduct.Id);
            publishedProduct.ShouldNotBeNull();

            publishedProduct.IsPublished.ShouldBe(false);

        }
    }
}