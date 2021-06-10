using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Categories
{
    public class CategoryManager : DomainService, ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> CreateAsync(string name, string description)
        {
            var categoryExists = _categoryRepository.FirstOrDefault(x => x.Name == name);
            
            if (categoryExists != null) throw new CategoryAlreadyExistException(name);
            
            var category = Category.Create(GuidGenerator.Create(), name, description);

            return await _categoryRepository.InsertAsync(category, true);
        }
    }
}