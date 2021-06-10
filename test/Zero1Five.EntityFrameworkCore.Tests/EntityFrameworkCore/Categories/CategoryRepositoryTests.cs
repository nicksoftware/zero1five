using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Zero1Five.EntityFrameworkCore;

namespace Zero1Five.Categories
{
    public class CategoryRepositoryTests : Zero1FiveEntityFrameworkCoreTestBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryRepositoryTests()
        {
            categoryRepository = GetRequiredService<ICategoryRepository>();
        }

        [Fact]
        public async Task Should_Query_Category()
        {

           var result =  await WithUnitOfWorkAsync<Category>(async () =>
            {
                //Act
                var cookingCategory = await (await categoryRepository.GetQueryableAsync())
                    .Where(u => u.Name == "Cooking")
                    .FirstOrDefaultAsync();
                //Assert
                return cookingCategory;
            });
            result.ShouldNotBeNull();
        }
    }
}