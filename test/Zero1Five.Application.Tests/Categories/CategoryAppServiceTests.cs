using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Xunit;
using Zero1Five;

namespace Zero1Five.Categories
{
    public class CategoryAppServiceTests : Zero1FiveApplicationTestBase
    {
        private readonly ICategoryAppService categoryAppService;
        public CategoryAppServiceTests()
        {
            categoryAppService = GetRequiredService<ICategoryAppService>();

        }

        [Fact]
        public async Task CreateCategoryAsync()
        {
            //Given
            var input = new CreateUpdateCategoryDto
            {
                Name = "Software Development",
                Description = "Software Development Description"
            };

            var result = await categoryAppService.CreateAsync(input);
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe(input.Name);
            result.Description.ShouldBe(input.Description);
        }

        [Fact]
        public async Task GetListAsync()
        {
            //Given
            var input = new PagedAndSortedResultRequestDto();

            //When
            var results = await categoryAppService.GetListAsync(input);
            //Then
            results.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GetAsync()
        {
            var categories = (await categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
            var categoryId = categories[0].Id;
            var result = await categoryAppService.GetAsync(categoryId);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(categoryId);
            result.Name.ShouldNotBeNullOrWhiteSpace();
            result.Description.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task UpdateAsync()
        {
            //Given
            var categories = (await categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
            var category = categories[0];

            var input = new CreateUpdateCategoryDto
            {
                Name = "New Name",
                Description = "new Name Description"
            };
            //When
            var result = await categoryAppService.UpdateAsync(category.Id, input);
            //Then
            result.Id.ShouldBe(category.Id);
            result.Name.ShouldBe(input.Name);
            result.Description.ShouldBe(input.Description);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            //Given
            var categories = (await categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
            var category = categories[0];
            //When
            await categoryAppService.DeleteAsync(category.Id);
            var results = (await categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
            //Then
            results.ShouldNotContain(category);
        }


    }
}