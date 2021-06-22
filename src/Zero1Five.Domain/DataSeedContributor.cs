using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zero1Five.Categories;

namespace Zero1Five
{
    public class DataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ICategoryRepository categoryRepository;

        public DataSeedContributor(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedCategories(context);

        }

        private async Task SeedCategories(DataSeedContext context)
        {
            if (await categoryRepository.GetCountAsync() <= 0)
            {
                var names = new[] { "Software Development", "Digital Marketing","Design" ,"Food"};
                List<Category> categories = new();

                for (var x = 0; x < names.Length; x++)
                {
                    var name = names[x];
                    categories.Add(Category.Create(Guid.NewGuid(), name, $"{name} description"));
                }
                await categoryRepository.InsertManyAsync(categories, true);
            }
        }
    }
}