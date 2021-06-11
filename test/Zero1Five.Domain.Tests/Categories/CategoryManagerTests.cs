using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace Zero1Five.Categories
{
    public class CategoryManagerTests:Zero1FiveDomainTestBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryManager _categoryManager;
        public CategoryManagerTests()
        {
            _categoryRepository = GetRequiredService<ICategoryRepository>();
            _categoryManager = GetRequiredService<ICategoryManager>();
        }

        [Fact]
        public async Task CreateAsync_GivenUniqueName_ShouldCreate_Category()
        {
            var name = "NIceTest";
            var description = "some description";
            var manager = new CategoryManager(_categoryRepository);
            var result =await WithUnitOfWorkAsync(() => _categoryManager.CreateAsync(name, description));
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe(name);
            result.Description.ShouldBe(description);
        }
        
        [Fact(Skip="Faulty Test")]
        public async Task CreateAsync_GivenExistingName_Should_ThrowException()
        {
            var name = "Marketing";
            var description = "some description";
            
            await Assert.ThrowsAsync<CategoryAlreadyExistException>(async () =>
            {
                 await WithUnitOfWorkAsync(() => _categoryManager.CreateAsync(name, description));
            });
        }
    }
}