using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Categories
{
    public class CategoryManager : DomainService, ICategoryManager
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<Category> CreateCategoryAsync(string name, string descrption)
        {
            var category = Category.Create(GuidGenerator.Create(), name, descrption);

            return await categoryRepository.InsertAsync(category, true);
        }
    }
}